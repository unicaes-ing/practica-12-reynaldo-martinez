using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace guia12
{
    [Serializable]
    public struct Alumno
    {

        public string carnet;
        public string nombre;
        public string carrera;
        private double cum;
    }
    class Program
    {
        static void Main(string[] args)
        {//switch para ejercicios

            int opc = 0;
            Console.WriteLine("Ejercicios 1-2");
            opc = Convert.ToInt32(Console.ReadLine());
            switch (opc)
            {

                case 1:
                    Console.Clear();
                    Mascota mascota = new Mascota();
                    FileStream fs;
                    BinaryFormatter formatter = new BinaryFormatter();
                    const string NOMBRE_ARCHIVO = "mascota.bin";
                    try
                    {
                        Console.WriteLine("******DATOS DE MASCOTAS*******");
                        Console.WriteLine();
                        Console.Write("Nombre: ");
                        mascota.nombre = Console.ReadLine();
                        Console.Write("Especie: ");
                        mascota.especie = Console.ReadLine();
                        Console.Write("Raza: ");
                        mascota.raza = Console.ReadLine();
                        Console.Write("Sexo: ");
                        mascota.sexo = Console.ReadLine();
                        Console.Write("Edad: ");
                        mascota.setEdad(Convert.ToInt32(Console.ReadLine()));
                        fs = new FileStream(NOMBRE_ARCHIVO, FileMode.Create, FileAccess.Write);
                        formatter.Serialize(fs, mascota);
                        fs.Close();
                        Console.WriteLine();
                        Console.WriteLine("La Mascota fue almacenado correctamente...");
                    }
                    catch (Exception e)
                    {

                        Console.WriteLine(e.Message);
                    }

                    //deserialize

                    fs = new FileStream(NOMBRE_ARCHIVO, FileMode.Open, FileAccess.Read);
                    var mascotareesp = (Mascota)formatter.Deserialize(fs);
                    Console.WriteLine("Nombre de mascota: ");
                    Console.WriteLine(mascotareesp.nombre);
                    Console.WriteLine("Especie de mascota: ");
                    Console.WriteLine(mascotareesp.especie);
                    Console.WriteLine("Raza de mascota: ");
                    Console.WriteLine(mascotareesp.raza);
                    Console.WriteLine("Sexo de mascota: ");
                    Console.WriteLine(mascotareesp.sexo);
                    Console.WriteLine("Edad de mascota: ");
                    Console.WriteLine(mascotareesp.edad);


                    fs.Close();

                    break;

                case 2:
                    Console.Clear();
                    Program p = new Program();
                    int opc2 = 0;
                    Console.WriteLine("1. Agregar alumno");
                    Console.WriteLine("2. Mostrar alumnos");
                    Console.WriteLine("3. Buscar alumno");
                    Console.WriteLine("4. editar alumno");
                    Console.WriteLine("5. Agregar alumno");
                    opc2 = Convert.ToInt32(Console.ReadLine());
                    switch (opc2)
                    {
                        case 1:
                            Console.Clear();
                            p.agregarAlumno();
                            break;
                        case 2:
                            Console.Clear();
                            p.mostrarDic();
                            break;
                        case 3:
                            Console.Clear();
                            p.buscarAlumno();
                            break;
                        case 4:
                            Console.Clear();
                            p.editarAlumno();
                            break;
                        case 5:
                            Console.Clear();
                            p.eliminarAlum();
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            
            
           
            Console.ReadKey();
        }


        private Dictionary<string, Alumno> dicAlumno = new Dictionary<string, Alumno>();
        private BinaryFormatter formater = new BinaryFormatter();
        private const string archivo = "alumnos.bin";





        public void agregarAlumno()
        {
            Alumno alumno = new Alumno();
            Console.WriteLine("Nombre: ");
            alumno.nombre = Console.ReadLine();
            Console.WriteLine("Carnet: ");
            alumno.carnet = Console.ReadLine();
            Console.WriteLine("Carrera: ");
            alumno.carrera = Console.ReadLine();
            Console.WriteLine("CUM");
            alumno.cum = Convert.ToDouble(Console.ReadLine());
           
            dicAlumno.Add(alumno.carnet, alumno);
            guardarDic(dicAlumno);

        }
        public bool guardarDic(Dictionary<string, Alumno> dic)
        {

            FileStream fs = new FileStream(archivo, FileMode.Create, FileAccess.Write);
            try
            {

                formater.Serialize(fs, dic);
                fs.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                fs.Close();
                Console.WriteLine("No se guardo");
                return false;

            }
        }

        public bool mostrarDic()
        {
            FileStream fs = new FileStream(archivo, FileMode.Open, FileAccess.Read);
            try
            {

                dicAlumno = (Dictionary<string, Alumno>)formater.Deserialize(fs);
                fs.Close();

                Console.WriteLine("alumnos");
                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("{0, 6} {1, 12} {2, 20} {0, 25}", "Carnet", "nombre", "Carrera", "CUM");
                foreach (KeyValuePair<string, Alumno> item in dicAlumno)
                {
                    Alumno alum = item.Value;
                    Console.WriteLine("{0, 6} {1, 12} {2, 20} {0, 25}", alum.carnet, alum.nombre, alum.carrera, alum.cum);
                }
                return true;
            }
            catch (Exception)
            {
                fs.Close();
                return false;

            }

        }

        public void buscarAlumno()
        {
            Console.WriteLine("Coloque el carnet del alumno a buscar");
            string carnet = Console.ReadLine();

            if (dicAlumno.ContainsKey(carnet))
            {
                Console.WriteLine("Este alumno existe en el diccionario");
            }
            else
            {
                Console.WriteLine("El alumno no fue encontrado");
            }
        }

        public void editarAlumno()
        {
            Console.WriteLine("Coloque el carnet del alumno que desea editar");
            string carnet = Console.ReadLine();

            if (dicAlumno.ContainsKey(carnet))
            {
                var alum = dicAlumno[carnet];

                Console.WriteLine("Coloque el nuevo nombre:");
                alum.nombre = Console.ReadLine();
                Console.WriteLine("Coloque la nueva carrera");
                alum.carrera = Console.ReadLine();
                Console.WriteLine("Coloque el nuevo carnet");
                alum.carnet = Console.ReadLine();
                Console.WriteLine("Coloque el nuevo cum");
                alum.cum = Convert.ToDouble(Console.ReadLine());
                FileStream fs = new FileStream(archivo, FileMode.Open, FileAccess.Write);

                formater.Serialize(fs, alum);


            }
        }

        public void eliminarAlum()
        {
            Console.WriteLine("Coloque el carnet del alumno que desea eliminar");
            string carnet = Console.ReadLine();

            if (dicAlumno.ContainsKey(carnet))
            {
                var alum = dicAlumno[carnet];
                dicAlumno.Remove(carnet);
            }
        }

        [Serializable]
        public struct Mascota
        {
            public string nombre;
            public string especie;
            public string raza;
            public string sexo;
            public int edad;
            public void setEdad(int edad)
            {
                if (edad > 0)
                {
                    this.edad = edad;
                }
            }
            public int getEdad()
            {
                return edad;
            }
        }

       
            


        
    
    }
}
