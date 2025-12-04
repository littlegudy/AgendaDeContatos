using AgendaDeContatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AgendaDeContatos.Repositories
{
    internal class ContatoPersistenciaArquivo
    {

        private readonly string _caminhoArquivo;

        public ContatoPersistenciaArquivo()
        {
            string pastaDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            _caminhoArquivo = Path.Combine(pastaDocuments, "contatos.txt");
        }

        public void SalvarContatos(List<Contato> contatos)
        {
            try
            {
                var linhas = new List<string>();

                foreach (var contato in contatos)
                {
                    string linha = $"{contato.Id}|{contato.Nome}|{contato.Telefone}|{contato.Email}";
                    linhas.Add(linha);
                }

                File.WriteAllLines(_caminhoArquivo, linhas);
                Console.WriteLine($"Dados salvos em {_caminhoArquivo}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar contatos: {ex.Message}");
            }
        }

        public List<Contato> CarregarContatos()
        {
            var contatos = new List<Contato>();
            try
            {
                if (!File.Exists(_caminhoArquivo))
                {
                    return contatos;
                }

                string[] linhas = File.ReadAllLines(_caminhoArquivo);

                foreach (var linha in linhas)
                {
                    string[] parte = linha.Split('|');

                    if (parte.Length >= 4)
                    {
                        var contato = new Contato
                        {
                            Id = int.Parse(parte[0]),
                            Nome = parte[1],
                            Telefone = parte[2],
                            Email = parte[3]
                        };

                        contatos.Add(contato);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar arquivo: {ex.Message}");
            }

            return contatos;

        }

    }
}
