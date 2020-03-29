using System;
using System.IO;

namespace Simulation
{
    class Caretaker
    {
        private string path;
        StreamWriter file;

        public Caretaker()
        {
            string current_path = Directory.GetCurrentDirectory();
            string saves_dir = System.IO.Path.Combine(current_path, "../../saves");
            Directory.CreateDirectory("../../saves");

            this.path = System.IO.Path.Combine(saves_dir, "sim.txt");;            
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
