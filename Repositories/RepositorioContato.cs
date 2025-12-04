using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgendaDeContatos.Models;

namespace AgendaDeContatos.Repositories;

internal class RepositorioContato
{

    private static RepositorioContato _instancia;

    private List<Contato> contatos = new List<Contato>();

    private ContatoPersistenciaArquivo fileHandler;
    public static RepositorioContato Instancia
    {
        get
        {
            if (_instancia == null)
            {
                _instancia = new RepositorioContato();
            }
            return _instancia;
        }
    } 
    private RepositorioContato()
    {
        fileHandler = new ContatoPersistenciaArquivo();
        contatos = fileHandler.CarregarContatos();
    }

    private void SalvarNoArquivo()
    {
        fileHandler.SalvarContatos(contatos);
    }


    private int GerarNovoId()
    {
        return contatos.Count > 0 ? contatos.Max(c => c.Id) + 1 : 1;
    }
    public void AdicionarContato(Contato contato)
    {
        contato.Id = GerarNovoId();
        contatos.Add(contato);
        SalvarNoArquivo();
    }

    public List<Contato> GetAll()
    {
        return contatos.ToList();
    }

    public Contato? GetById(int id)
    {
        return contatos.FirstOrDefault(c => c.Id == id);
    }

    public void Update(Contato contatoAtualizado)
    {
        var c = GetById(contatoAtualizado.Id);
        if (c != null)
        {
            c.Nome = contatoAtualizado.Nome;
            c.Telefone = contatoAtualizado.Telefone;
            c.Email = contatoAtualizado.Email;

            SalvarNoArquivo();
        }
    }

    public void Delete(int id)
    {
        var contato = GetById(id);
        if (contato != null)
        {
            contatos.Remove(contato);
            SalvarNoArquivo();
        }
    }
}
