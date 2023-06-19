using People.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People
{
    public class PersonRepository
    {
        string _dbPath;

        public string StatusMessage { get; set; }

        // agregamos la conexion con una variable connection
        private SQLiteConnection conn;

        private void Init()
        {
            // para inicializar el repositorio primero evaluamos que la conexión no este en nulo
            if (conn is not null)
                return;
            // si esta en nulo asignamos un nuevo valor con la ruta definida arriba
            conn = new(_dbPath);
            // creamos la tabla 
            conn.CreateTable<Person>();
        }

        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;                        
        }

        // generamos el metodo para insertar una nueva persona
        public void AddNewPerson(string name)
        {            
            int result = 0;
            try
            {
                // convocamos al constructor
                Init();

                // validamos que el nombre no este en blanco
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");
                // generamos una nueva persona
                Person person = new() { Name = name };
                // de esta forma se inserta
                result = conn.Insert(person);

                StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
            }

        }

        // obtener la informacion de los datos que hemos generado
        public List<Person> GetAllPeople()
        {
            // llamamos al Init
            
            try
            {
                Init();
                // queremos que nos devuelva la tabla de Person y que lo convierta en una lista
                return conn.Table<Person>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Person>();
        }
    }
}
