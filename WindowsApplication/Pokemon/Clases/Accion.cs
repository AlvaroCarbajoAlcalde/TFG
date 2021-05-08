namespace Pokemon
{
    public class Accion
    {

        #region Propiedades

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

        #endregion

        #region Constructores

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

        #endregion

        #region Metodos

        public string Mostrar()
        {
            switch (tipoAccion)
            {
                case TipoAccion.ATAQUE:
                    return $"Ataque {ataqueUsado.nombre}";
                case TipoAccion.CAMBIOPOKEMON:
                    return $"Cambio {pokemonACambiar.nombre}";
                case TipoAccion.OBJETO:
                    return $"Objeto {objetoUsado.nombre}";
            }
            return "NULL";
        }

        #endregion

    }
}
