
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Ashes
{
    public class Layer<T> where T : Displayable
    {
        protected Displayable[,] _layer;

        public Layer(int boardWidth, int boardHeight)
        {
            _layer = new Displayable[boardWidth, boardHeight];
        }

        public Layer(int boardWidth, int boardHeight, Dictionary<Type, int> typeCounts) : this(boardWidth, boardHeight)
        {
            int sumOfCounts = typeCounts.Sum(p => p.Value);
           if (sumOfCounts > boardWidth * boardHeight)
                throw new TooManyElementsToPutOnLayerException();
            for (int i = 0; i < sumOfCounts; i++)
            {
                var firstNonZero = typeCounts.First(p => p.Value > 0);
                typeCounts[firstNonZero.Key]--;
                int x = i % boardHeight;
                int y = i / boardHeight;
                var obj = (T)Activator.CreateInstance(firstNonZero.Key)!;
                _layer[x, y] = obj;
                obj.X = x;
                obj.Y = y;
            }
        }
        public void AddCharacter(Type type, int row, int column) 
        {
            var obj = (T)Activator.CreateInstance(type)!;
            _layer[column, row] = obj;
            obj.X = column;
            obj.Y = row;

        }

        public virtual void Shuffle()
        {
            Random random = new Random();

            for (int i = 0; i < _layer.GetLength(0) * _layer.GetLength(1) - 1; i++)
            {
                int baseX = i % _layer.GetLength(1);
                int baseY = i / _layer.GetLength(1);

                int j = random.Next(i + 1, _layer.GetLength(0) * _layer.GetLength(1));
                int changeX = j % _layer.GetLength(1);
                int changeY = j / _layer.GetLength(1);

                Displayable tmp = _layer[baseX, baseY];
                _layer[baseX, baseY] = _layer[changeX, changeY];
                _layer[changeX, changeY] = tmp;

                if (_layer[baseX, baseY] != null)
                {
                    _layer[baseX, baseY].X = baseX;
                    _layer[baseX, baseY].Y = baseY;
                }
                if (_layer[changeX, changeY] != null)
                {
                    _layer[changeX, changeY].X = changeX;
                    _layer[changeX, changeY].Y = changeY;
                }
            }
        }

        public virtual void Serialize()
        {
            string[,] types = new string[_layer.GetLength(0), _layer.GetLength(1)];
            for (int x = 0; x < _layer.GetLength(0); x++)
                for (int y = 0; y < _layer.GetLength(1); y++)
                    types[x, y] = _layer[x, y]?.GetType().FullName;

            var formatter = new BinaryFormatter();
            using (Stream stream = new FileStream($"layer-{typeof(T).Name}.bin", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, types);
            }
        }

        public virtual void Deserialize()
        {
            var formatter = new BinaryFormatter();
            using (Stream stream = new FileStream($"layer-{typeof(T).Name}.bin", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                string[,] types = (string[,])formatter.Deserialize(stream);
                for (int i = 0; i < types.GetLength(0); i++)
                    for (int j = 0; j < types.GetLength(1); j++)
                    {
                        var type = Assembly.GetExecutingAssembly()
                                           .GetTypes()
                                           .FirstOrDefault(t => t.FullName == types[i, j]);
                        if (type == null)
                            continue;
                        _layer[i, j] = (T)Activator.CreateInstance(type);
                        _layer[i, j].X = i;
                        _layer[i, j].Y = j;
                    }
            }
        }


    }
}
