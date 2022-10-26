using System;
using System.IO;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace WindowsFormsApp1
{


    internal class Logger
    {

        private double fileSizeLimit = 264;  //tamanho em kb    1572864kb === 1.5mb  
        


        public string FileName { get; set; }

        public Logger ()
        {
            Random numAleatorio = new Random();
            FileName =  @"\" +"log -"+ DateTime.Today.ToString("d").Replace("/", ".") + "-" + numAleatorio.Next()+".txt";
        }

        /// <summary>
        /// Cria o arquivo com extensão .txt.
        /// </summary>

        private StreamWriter createFileLog()
        {

           
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + FileName;
            return new StreamWriter(filePath, true);
        }


        /// <summary>
        /// Registro dos eventos no arquivo
        /// </summary>
        public bool writeFileLog(string message)
        {
            StreamWriter file =  createFileLog();
            var register = new Register();
            var serializer = new JavaScriptSerializer();
            register.Date = DateTime.Now.ToString();
            register.Action = message;

            
            string serializedResult = serializer.Serialize(register);
            
            if (!maxFileSize())
            {
                file.WriteLine(serializedResult + ",");

            }
            else
            {
                file.WriteLine(serializedResult);
            }
           

            file.Dispose();
            
            return maxFileSize();
        }


        /// <summary>
        /// Verifica se o arquivo alcançou o tamanho (kb) máximo determinado
        /// </summary>
        public bool maxFileSize()
        {
          
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + FileName;
            FileInfo file = new FileInfo(filePath); // using System.IO;
            long fileSize = file.Length;
            Console.WriteLine(fileSize);
            if(fileSize >= fileSizeLimit)
            {
                MessageBox.Show("Arquivo alcançou o limite permitido determinado de " + fileSizeLimit / 1048576 + "mb"); 
                createFileLogJson();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Cria o arquivo no formato .json
        /// </summary>
        public void createFileLogJson()
        {
            string file = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + FileName);
            Console.WriteLine("Tamanho arquivo"+(file.Length-3));
            string trimFile=file.Remove(file.Length - 3);
            string jsonFile = "[" + trimFile + "]";
            //Console.WriteLine(jsonFile);
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + "fileName.json";
            File.WriteAllText(filePath, jsonFile);
        }

        /// <summary>
        /// Adiciona de forma automatizada um ùnico método para cada elemento que possui o evento Click
        /// </summary>
        /// <param name="Controls">Insira a propriedade Controls pertencente a classe Form</param>
        /// <param name="_HandleEvents">Insira a função gerada por meio da Interface IWindowsFormAppListener </param>
        public void addGeneralEvent(System.Windows.Forms.Control.ControlCollection Controls, Action<System.Object, System.EventArgs> _HandleEvents)
        {
            foreach (Control grandChild in Controls) grandChild.Click += new System.EventHandler(_HandleEvents);
        }

   
    }




    public class Register
    {
        public string Date { get; set; }
        public string Action { get; set; }
    }


}
