using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
    class Caretaker
    {
        private string path;
        StreamWriter file;

        public Caretaker()
        {
            this.path = @"C:\Users\danwo\Desktop\Studia\Semestr_5\TO\particle_move\Simulation\Simulation\saves\sim_save.txt";
            
                this.file = File.CreateText(path); 
        }

        public void SaveToFile(Memento mem)
        {
            file.WriteLine(mem.GetState());
        }

        public string[] ReadFromFile()
        {
            string[] data = new string[20];
            using (StreamReader reader = File.OpenText(path))
            {
                string line;
                int i = 0;
                while((line = reader.ReadLine()) != null)
                {
                    data[i] = line;
                    Console.WriteLine("Caretaker " + data[i]);
                    i++;
                }
            }
            return data;
        }

        public void CloseFile()
        {
            file.Close();
        }
    }
}
