using System.Text;

namespace DL.Common.Helper
{
    /// <summary>
    /// Copy from : https://github.com/duncanrhamill/RandomNameGen/blob/master/RandomNameGen/RandomName.cs
    /// </summary>
    public class NameGenerator
    {
        private List<string> _girls { get; set; }
        private List<string> _boys { get; set; }
        private List<string> _last { get; set; }

        Random rand;

        public NameGenerator(List<string> girls, List<string> boys, List<string> last)
        {
            _girls = girls;
            _boys = boys;
            _last = last;
            rand = new Random(); 
        }

        public List<string> RandomNames(int number, int maxMiddleNames, Sex? sex = null, bool? initials = null)
        {
            List<string> names = new List<string>();

            for (int i = 0; i < number; i++)
            {
                if (sex != null && initials != null)
                {
                    names.Add(Generate((Sex)sex, rand.Next(0, maxMiddleNames + 1), (bool)initials));
                }
                else if (sex != null)
                {
                    bool init = rand.Next(0, 2) != 0;
                    names.Add(Generate((Sex)sex, rand.Next(0, maxMiddleNames + 1), init));
                }
                else if (initials != null)
                {
                    Sex s = (Sex)rand.Next(0, 2);
                    names.Add(Generate(s, rand.Next(0, maxMiddleNames + 1), (bool)initials));
                }
                else
                {
                    Sex s = (Sex)rand.Next(0, 2);
                    bool init = rand.Next(0, 2) != 0;
                    names.Add(Generate(s, rand.Next(0, maxMiddleNames + 1), init));
                }
            }

            return names;
        }

        private string Generate(Sex sex, int middle = 0, bool isInital = false)
        {
            string first = sex == Sex.Male ? _boys[rand.Next(_boys.Count)] : _girls[rand.Next(_girls.Count)]; // determines if we should select a name from male or female, and randomly picks
            string last = _last[rand.Next(_last.Count)]; // gets the last name

            List<string> middles = new List<string>();

            for (int i = 0; i < middle; i++)
            {
                if (isInital)
                {
                    middles.Add("ABCDEFGHIJKLMNOPQRSTUVWXYZ"[rand.Next(0, 25)].ToString() + "."); // randomly selects an uppercase letter to use as the inital and appends a dot
                }
                else
                {
                    middles.Add(sex == Sex.Male ? _boys[rand.Next(_boys.Count)] : _girls[rand.Next(_girls.Count)]); // randomly selects a name that fits with the sex of the person
                }
            }

            StringBuilder b = new StringBuilder();
            b.Append(first + " "); // put a space after our names;
            foreach (string m in middles)
            {
                b.Append(m + " ");
            }
            b.Append(last);

            return b.ToString();
        }        

        public enum Sex
        {
            Male,
            Female
        }

    }
}
