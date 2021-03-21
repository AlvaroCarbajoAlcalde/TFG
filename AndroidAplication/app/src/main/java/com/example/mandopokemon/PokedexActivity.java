package com.example.mandopokemon;

import androidx.appcompat.app.AppCompatActivity;

import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

import com.example.mandopokemon.SQL.ConexionSQLiteHelper;
import com.example.mandopokemon.Utiles.UtilImagenes;

import static com.example.mandopokemon.SQL.ConexionSQLiteHelper.*;

public class PokedexActivity extends AppCompatActivity {

    private ConexionSQLiteHelper conn  = new ConexionSQLiteHelper(this, "bd", null, 1);;

    private TextView txtViewNombre, txtViewDescripcion;
    private ImageView ivPokemon, ivTipo1, ivTipo2;

    private int n;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_pokedex);

        ivPokemon = findViewById(R.id.imageViewPokemon);
        ivTipo1 = findViewById(R.id.ivTipo1);
        ivTipo2 = findViewById(R.id.ivTipo2);
        txtViewNombre = findViewById(R.id.txtViewNombre);
        txtViewDescripcion = findViewById(R.id.txtViewDescripcion);

        n = 1;
        setDatosPokemon(n);
    }

    /**
     * Cambia los datos de la vista para ajustarlos al pokeomon
     *
     * @param id id del pokemon para realizar la consulta
     */
    public void setDatosPokemon(int id) {
        //Realizamos consulta
        SQLiteDatabase db = conn.getReadableDatabase();
        String consulta = String.format("SELECT * FROM %s WHERE %s=%d", NOMBRE_TABLA_POKEMON, CAMPO_POKEMON_ID, id);
        Cursor c = db.rawQuery(consulta, null);

        if (c.moveToFirst()) {
            //Establecemos los datos
            txtViewNombre.setText(c.getString(c.getColumnIndex(CAMPO_POKEMON_NOMBRE)));
            txtViewDescripcion.setText(c.getInt(c.getColumnIndex(CAMPO_POKEMON_DESCRIPCION)));
            ivPokemon.setImageResource(UtilImagenes.getIdRecursoPokemon(c.getInt(c.getColumnIndex(CAMPO_POKEMON_ID))));
            ivTipo1.setImageResource(UtilImagenes.getIdRecursoTipo(c.getInt(c.getColumnIndex(CAMPO_POKEMON_TIPO_1))));

            //Si solo hay un tipo invisibilizamos el otro
            int tipo2 = UtilImagenes.getIdRecursoTipo(c.getInt(c.getColumnIndex(CAMPO_POKEMON_TIPO_2)));
            if (tipo2 != Integer.MAX_VALUE) {
                ivTipo2.setImageResource(tipo2);
                ivTipo2.setVisibility(View.VISIBLE);
            } else {
                ivTipo2.setVisibility(View.INVISIBLE);
            }
        }

        db.close();
    }

}