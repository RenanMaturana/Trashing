using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BasureroWeb.Models;
using System.Security.Cryptography;
using System.Text;

namespace BasureroWeb.Querys
{
    public class QueryUsuario
    {
        basureroEntities db = new basureroEntities();

        public bool eliminarUsuario(string rut)
        {
            try {
                var user = db.usuario.Find(rut);
                db.usuario.Remove(user);
                int resp = db.SaveChanges();

                return resp == 1;
            } catch(Exception) {

                return false;
            }

        }


        public bool addCargo(cargo c)
        {
            try {
                db.cargo.Add(c);
                db.SaveChanges();
                int resp = db.SaveChanges();
                return resp == 1;
            } catch(Exception) {

                return false;
            }

        }

        public bool addCiudad(ciudad c)
        {
            try {
                db.ciudad.Add(c);
                db.SaveChanges();
                int resp = db.SaveChanges();
                return resp == 1;
            } catch(Exception) {

                return false;
            }

        }
        public bool addEstado(estado c)
        {
            try {
                db.estado.Add(c);
                db.SaveChanges();
                int resp = db.SaveChanges();
                return resp == 1;
            } catch(Exception) {

                return false;
            }

        }


    }
}