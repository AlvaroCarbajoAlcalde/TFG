using System;

namespace Pokemon
{
    public class Accion
    {
        public enum TipoAccion
        {
            ATAQUE = 0,
            CAMBIOPOKEMON = 1,
            OBJETO = 2
        };

        public TipoAccion tipoAccion;
        public Pokemon pokemonACambiar;
        public Ataque ataqueUsado;
        public Objeto objetoUsado;

        public Accion(Pokemon pokemonACambiar)
        {
            tipoAccion = TipoAccion.CAMBIOPOKEMON;
            this.pokemonACambiar = pokemonACambiar;
        }

        public Accion(Objeto objetoUsado)
        {
            tipoAccion = TipoAccion.OBJETO;
            this.objetoUsado = objetoUsado;
        }

        public Accion(Ataque ataqueUsado)
        {
            tipoAccion = TipoAccion.ATAQUE;
            this.ataqueUsado = ataqueUsado;
        }

        public string Mostrar()
        {
            switch (tipoAccion)
            {
                case TipoAccion.ATAQUE:
                    return "Ataque " + ataqueUsado.nombre;
                case TipoAccion.CAMBIOPOKEMON:
                    return "Cambio " + pokemonACambiar.nombre;
                case TipoAccion.OBJETO:
                    return "Objeto " + objetoUsado.nombre;
            }
            return "NULL";
        }
    }
}
