using Autobase.Models.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Autobase.Utils
{
    public class RandomUtil
    {
        private static RandomUtil instance = new RandomUtil();
        private List<char> alphabet;
        private Random rand;
        private int length;

        private RandomUtil()
        {
            rand = new Random();
            InitAlphaber();
        }

        public static RandomUtil GetInstance
        {
            get
            {
                return (instance == null) ? new RandomUtil() : instance;
            }
        }

        public string GetRandomString
        {
            get
            {
                StringBuilder str = new StringBuilder();
                for (int i = 0; i < rand.Next(2, 15); i++)
                {
                    str.Append(alphabet[rand.Next(length - 1)]);
                }
                return str.ToString();
            }
        }

        public int GetRandomInt
        {
            get
            {
                return Math.Abs(rand.Next());
            }
        }

        public double GetRandomDouble
        {
            get
            {
                return Math.Abs(rand.NextDouble());
            }
        }

        public bool GetRandomBool
        {
            get
            {
                return (rand.Next(0, 2) == 1);
            }
        }

        public TripStatusEnum GetRandomStatus
        {
            get
            {
                Array enumValues = Enum.GetValues(typeof(TripStatusEnum));
                int length = enumValues.Length;
                int randomIndex = rand.Next(0, 3);
                return (TripStatusEnum) enumValues.GetValue(randomIndex);
            }
        }

        public int GetRandomId
        {
            get
            {
                return rand.Next(1, 100);
            }
        }

        public DateTime GetRandomDate
        {
            get
            {
                return DateTime.UtcNow.AddDays(new Random().Next(90));
            }
        }

        private void InitAlphaber()
        {
            alphabet = new List<char>();
            for (char i = 'A'; i <= 'z'; i++)
            {
                alphabet.Add(i);
            }
            length = alphabet.Count;
        }
    }
}