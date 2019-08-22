using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace APIAutenticacao.Utils
{
    public class Common
    {
        // Gera um valor Salt string randomico
        public static byte[] getRandomSalt(int tamanho)
        {
            var random = new RNGCryptoServiceProvider();
            byte[] salt = new byte[tamanho];
            random.GetNonZeroBytes(salt);
            return salt;
        }
        // Cria uma senha com Salt
        public static byte[] SaltHashPassword(byte[] senha, byte[] salt)
        {
            HashAlgorithm algoritmo = new SHA256Managed();
            byte[] textoSimplesComSalt = new byte[senha.Length + salt.Length];
            for (int i = 0; i < senha.Length; i++)
                textoSimplesComSalt[i] = senha[i];
            for (int i = 0; i < salt.Length; i++)
                textoSimplesComSalt[senha.Length + i] = salt[i];
            return algoritmo.ComputeHash(textoSimplesComSalt);
        }

    }
}
