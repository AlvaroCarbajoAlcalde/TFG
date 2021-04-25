package com.example.mandopokemon;

import androidx.appcompat.app.AppCompatActivity;
import androidx.core.view.GestureDetectorCompat;

import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.view.GestureDetector;
import android.view.MotionEvent;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

import com.example.mandopokemon.SQL.ConexionSQLiteHelper;
import com.example.mandopokemon.Utiles.UtilImagenes;

import static com.example.mandopokemon.SQL.ConexionSQLiteHelper.*;

public class PokedexActivity extends AppCompatActivity {

    private ConexionSQLiteHelper conn = new ConexionSQLiteHelper(this, "bd", null, 1);

    private TextView txtViewNombre, txtViewDescripcion, txtViewCategoria, txtViewPeso, txtViewAltura;
    private ImageView ivPokemon, ivTipo1, ivTipo2;

    private GestureDetectorCompat gestureDetector;

    private int pokemonActual;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_pokedex);

        //Bundle
        Bundle entrega = this.getIntent().getExtras();
        pokemonActual =  entrega.getInt("id");

        initComponents();

        //Detector de gestos
        gestureDetector = new GestureDetectorCompat(this, new GestureListener());

        setDatosPokemon(pokemonActual);
    }

    /**
     * Inicializa los componentes
     */
    private void initComponents() {
        ivPokemon = findViewById(R.id.imageViewPokemon);
        ivTipo1 = findViewById(R.id.ivTipo1);
        ivTipo2 = findViewById(R.id.ivTipo2);
        txtViewNombre = findViewById(R.id.txtViewNombre);
        txtViewDescripcion = findViewById(R.id.txtViewDescripcion);
        txtViewCategoria = findViewById(R.id.txtViewCategoria);
        txtViewPeso = findViewById(R.id.txtViewPeso);
        txtViewAltura = findViewById(R.id.txtViewAltura);
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
            txtViewDescripcion.setText(c.getString(c.getColumnIndex(CAMPO_POKEMON_DESCRIPCION)));
            ivPokemon.setImageResource(UtilImagenes.getIdRecursoPokemon(c.getInt(c.getColumnIndex(CAMPO_POKEMON_ID))));
            ivTipo1.setImageResource(UtilImagenes.getIdRecursoTipo(c.getInt(c.getColumnIndex(CAMPO_POKEMON_TIPO_1))));
            txtViewAltura.setText("Altura: " + c.getString(c.getColumnIndex(CAMPO_POKEMON_ALTURA)));
            txtViewPeso.setText("Peso: " + c.getString(c.getColumnIndex(CAMPO_POKEMON_PESO)));
            txtViewCategoria.setText("Pokemon " + c.getString(c.getColumnIndex(CAMPO_POKEMON_CATEGORIA)));

            //Si solo hay un tipo invisibilizamos el otro
            int tipo2 = UtilImagenes.getIdRecursoTipo(c.getInt(c.getColumnIndex(CAMPO_POKEMON_TIPO_2)));
            if (tipo2 != Integer.MAX_VALUE) {
                ivTipo2.setImageResource(tipo2);
                ivTipo2.setVisibility(View.VISIBLE);
            } else {
                ivTipo2.setVisibility(View.GONE);
            }
        }

        db.close();
    }

    private class GestureListener extends GestureDetector.SimpleOnGestureListener {

        @Override
        //Cuando se hace un gesto se llama a este metodo
        public boolean onFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY) {
            //Comprobamos hacia que lado se hizo el gesto.
            if (velocityX < 0) {
                pokemonActual++;
                if (pokemonActual > 493) pokemonActual = 1;
            } else {
                pokemonActual--;
                if (pokemonActual < 1) pokemonActual = 493;
            }
            setDatosPokemon(pokemonActual);
            return super.onFling(e1, e2, velocityX, velocityX);
        }

    }

    @Override
    public boolean onTouchEvent(MotionEvent event) {
        gestureDetector.onTouchEvent(event);
        return super.onTouchEvent(event);
    }
}