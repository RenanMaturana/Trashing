using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BasureroWeb.Models;

namespace BasureroWeb.Querys
{
    public class QueryBasurero
    {
        basureroEntities db = new basureroEntities();

        public bool addUbicacion(ubicacion c)
        {
            try {
                db.ubicacion.Add(c);
                db.SaveChanges();
                int resp = db.SaveChanges();
                return resp == 1;
            } catch(Exception) {

                return false;
            }

        }

        public bool addBodega(bodega c)
        {
            try {
                db.bodega.Add(c);
                db.SaveChanges();
                int resp = db.SaveChanges();
                return resp == 1;
            } catch(Exception) {

                return false;
            }

        }

        public bool addEstadoB(estadobasurero c)
        {
            try {
                db.estadobasurero.Add(c);
                db.SaveChanges();
                int resp = db.SaveChanges();
                return resp == 1;
            } catch(Exception) {

                return false;
            }

        }

    }
}