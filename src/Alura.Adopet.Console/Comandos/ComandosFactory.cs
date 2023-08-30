﻿using Alura.Adopet.Console.Servicos.Http;
using Alura.Adopet.Console.Servicos.Arquivos;
using Alura.Adopet.Console.Servicos.Abstracoes;

namespace Alura.Adopet.Console.Comandos;

public static class ComandosFactory
{
    public static IComando? CriarComando(string[] argumentos)
    {
        if ((argumentos is null) || (argumentos.Length == 0))
        {
            return null;
        }
        var comando = argumentos[0];
        switch (comando)
        {
            case "import":
                var httpClientPet = new HttpClientPet(new AdopetAPIClientFactory().CreateClient("adopet"));
                var leitorDeArquivos = LeitorDeArquivoFactory.CreateLeitorDePets(argumentos[1]);
                if (leitorDeArquivos is null) return null;
                return new Import(httpClientPet, leitorDeArquivos);                    

            case "list":
                var httpClientPetList = new HttpClientPet(new AdopetAPIClientFactory().CreateClient("adopet"));
                return new List(httpClientPetList);               
            case "show":
                var leitorDeArquivosShow = LeitorDeArquivoFactory.CreateLeitorDePets (argumentos[1]);
                if (leitorDeArquivosShow is null) return null;
                return new Show(leitorDeArquivosShow);
            case "help":
                var comandoASerExibido = argumentos.Length==2? argumentos[1] : null;
                return new Help(comandoASerExibido);

            case "import-clientes":
                var serviceClientes = new ClienteService(new AdopetAPIClientFactory().CreateClient("adopet"));
                var leitorArquivoClientes = LeitorDeArquivoFactory.CreateLeitorDeClientes(argumentos[1]);
                if (leitorArquivoClientes is null) return null;
                return new ImportClientes(serviceClientes, leitorArquivoClientes);

            default: return null;
        }           
    }
}
