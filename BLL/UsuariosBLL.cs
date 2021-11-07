﻿using Microsoft.EntityFrameworkCore;
using RegistroLogin.DAL;
using RegistroLogin.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RegistroLogin.BLL
{
    public class UsuariosBLL
    {
        public static bool Guardar(Usuario Usuario)
        {
            if (!Existe(Usuario.UsuarioId))
                return Insertar(Usuario);
            else
                return Modificar(Usuario);
        }
        private static bool Insertar(Usuario Usuario)
        {
            bool paso = false;
            //Creamos una instancia del contexto para poder conectar con la DB
            Contexto db = new Contexto();
            try
            {
                if (db.Usuario.Add(Usuario) != null)
                    paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            { throw; }
            finally
            {
                db.Dispose();//Cerramos la conexion
            }
            return paso;
        }
        /// <summary>
        /// Permite Modificar una entidad en la base de datos
        /// </summary>
        private static bool Modificar(Usuario Usuario)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                db.Entry(Usuario).State = EntityState.Modified;
                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            { throw; }
            finally
            {
                db.Dispose();//Cerramos la conexion
            }
            return paso;
        }
        /// <summary>
        /// Permite Eliminar una entidad en la base de datos
        /// </summary>
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                Usuario Usuario = db.Usuario.Find(id);

                if (Existe(id))
                {
                    db.Usuario.Remove(Usuario);
                    paso = db.SaveChanges() > 0;
                }

            }
            catch (Exception)
            { throw; }
            finally
            {
                db.Dispose();//Cerramos la conexion
            }
            return paso;
        }
        /// <summary>
        /// Permite Buscar una entidad en la base de datos
        /// </summary>
        public static Usuario Buscar(int id)
        {
            Contexto db = new Contexto();
            Usuario Usuario = new Usuario();
            try
            {
                Usuario = db.Usuario.Find(id);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return Usuario;
        }
        /// <summary>
        /// Permite Buscar una entidad en la base de datos
        /// </summary>
        public static Usuario BuscarPorAlias(string Alias)
        {
            Contexto db = new Contexto();
            Usuario Usuario = new Usuario();
            try
            {
                Usuario = db.Usuario.Where(x => Alias.ToLower() == x.Alias.ToLower()).SingleOrDefault();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return Usuario;
        }
        /// <summary>
        /// Permite extraer una lista de Usuario de la base de datos
        /// </summary>
        public static List<Usuario> GetList(Expression<Func<Usuario, bool>> expression)
        {
            List<Usuario> Usuario = new List<Usuario>();
            Contexto db = new Contexto();
            try
            {
                Usuario = db.Usuario.Where(expression).ToList();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return Usuario;
        }
        private static bool Existe(int id)
        {
            bool encontrado = false;
            Contexto db = new Contexto();
            try
            {
                encontrado = db.Usuario.Any(x => x.UsuarioId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return encontrado;
        }
        public static bool Login(string Alias, string contraseña)
        {
            Usuario usuario = BuscarPorAlias(Alias);

            if (usuario != null)
            {
                return usuario.Clave.Equals(contraseña);
            }
            else
                return false;

        }
        public static string SHA1(string contraseña)
        {
            using (SHA1Managed SHa1 = new SHA1Managed())
            {
                var hash = SHa1.ComputeHash(Encoding.UTF8.GetBytes(contraseña));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte item in hash)
                {
                    sb.Append(item.ToString("X2"));
                }
                return sb.ToString();
            }
        }

    }
}
