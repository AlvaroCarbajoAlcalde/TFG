package com.example.mandopokemon;

import androidx.appcompat.app.AppCompatActivity;

import android.content.ContentValues;
import android.content.Intent;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.TextView;

import com.example.mandopokemon.Modelo.PokedexPokemon;
import com.example.mandopokemon.SQL.ConexionSQLiteHelper;

import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;
import org.xml.sax.SAXException;

import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.DataInputStream;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;

import static com.example.mandopokemon.SQL.ConexionSQLiteHelper.CAMPO_POKEMON_ALTURA;
import static com.example.mandopokemon.SQL.ConexionSQLiteHelper.CAMPO_POKEMON_CATEGORIA;
import static com.example.mandopokemon.SQL.ConexionSQLiteHelper.CAMPO_POKEMON_DESCRIPCION;
import static com.example.mandopokemon.SQL.ConexionSQLiteHelper.CAMPO_POKEMON_ID;
import static com.example.mandopokemon.SQL.ConexionSQLiteHelper.CAMPO_POKEMON_NOMBRE;
import static com.example.mandopokemon.SQL.ConexionSQLiteHelper.CAMPO_POKEMON_PESO;
import static com.example.mandopokemon.SQL.ConexionSQLiteHelper.CAMPO_POKEMON_TIPO_1;
import static com.example.mandopokemon.SQL.ConexionSQLiteHelper.CAMPO_POKEMON_TIPO_2;
import static com.example.mandopokemon.SQL.ConexionSQLiteHelper.NOMBRE_TABLA_POKEMON;

public class PokedexListaActivity extends AppCompatActivity {

    private ConexionSQLiteHelper conn = new ConexionSQLiteHelper(this, "bd", null, 1);

    private TextView txtViewCargando;
    private ListView listaPokemon;
    private ArrayList<VistaLista> arrayListVista;
    private ArrayAdapter adaptador;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_pokedex_lista);

        //Inicializar
        txtViewCargando = findViewById(R.id.textViewCargando);
        listaPokemon = findViewById(R.id.listViewPokedex);
        arrayListVista = new ArrayList<>();

        //Adaptador de la lista
        adaptador  = new ArrayAdapter(this, android.R.layout.simple_list_item_1, arrayListVista);

        //Thread para leer datos.
        Thread thread = new Thread(new Runnable() {
            @Override
            public void run() {
                try {
                    //Necesario para que no se produzca excepcion de cambiar vistas desde hilo
                    runOnUiThread(new Runnable() {
                        @Override
                        public void run() {

                            //Upgrade a los datos en la base de datos
                            conn.onUpgrade(conn.getWritableDatabase(), 1, 2);
                            leerDatosXML();
                            txtViewCargando.setVisibility(View.GONE);
                        }
                    });
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
        thread.start();

        //Click listener de la lista
        listaPokemon.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                Intent intent = new Intent(view.getContext(), PokedexActivity.class);
                Bundle bundle = new Bundle();
                bundle.putInt("id", position + 1);
                intent.putExtras(bundle);
                startActivityForResult(intent, 0);
            }
        });
    }

    /**
     * Lee los datos del xml para insertarlos en la base de datos
     */
    public void leerDatosXML() {
        PokedexPokemon pokemon = new PokedexPokemon();
        try {
            DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
            DocumentBuilder builder = factory.newDocumentBuilder();
            // Parseamos el documento y lo almacenamos en un objeto Document
            Document doc = builder.parse(getResources().openRawResource(R.raw.pokedex));

            // Obtenemos el elemento raiz del documento, pokemons
            Element raiz = doc.getDocumentElement();

            // Obtenemos todos los elementos llamados pokemon, que cuelgan de la raiz
            NodeList items = raiz.getElementsByTagName("Pokemon");

            // Recorremos todos los elementos obtenidos
            for (int i = 0; i < items.getLength(); i++) {
                Node nodoPokemon = items.item(i);

                // Recorremos todos los hijos que tenga el nodo pokemon
                for (int j = 0; j < nodoPokemon.getChildNodes().getLength(); j++) {
                    Node nodoActual = nodoPokemon.getChildNodes().item(j);

                    // Compruebo si es un elemento
                    if (nodoActual.getNodeType() == Node.ELEMENT_NODE) {
                        switch (nodoActual.getNodeName().toLowerCase()) {
                            case "idpok":
                                pokemon.setId(Integer.parseInt(nodoActual.getChildNodes().item(0).getNodeValue()));
                                break;
                            case "nombre":
                                pokemon.setNombre(nodoActual.getChildNodes().item(0).getNodeValue());
                                break;
                            case "tipo1":
                                pokemon.setTipo1(Integer.parseInt(nodoActual.getChildNodes().item(0).getNodeValue()));
                                break;
                            case "tipo2":
                                pokemon.setTipo2(Integer.parseInt(nodoActual.getChildNodes().item(0).getNodeValue()));
                                break;
                            case "categoria":
                                pokemon.setCategoria(nodoActual.getChildNodes().item(0).getNodeValue());
                                break;
                            case "descripcion":
                                pokemon.setDescripcion(nodoActual.getChildNodes().item(0).getNodeValue());
                                break;
                            case "peso":
                                pokemon.setPeso(nodoActual.getChildNodes().item(0).getNodeValue());
                                break;
                            case "altura":
                                pokemon.setAltura(nodoActual.getChildNodes().item(0).getNodeValue());
                                break;
                        }
                    }
                }
                //Insertamos esos datos
                arrayListVista.add(new VistaLista(pokemon.getId(), pokemon.getNombre()));
                listaPokemon.setAdapter(adaptador);
                insertarDatos(pokemon);
            }

        } catch (ParserConfigurationException | SAXException | IOException e) {
            System.out.println("ERROR al leer XML: " + e.getMessage());
        }
    }

    /**
     * Inserta datos en la base de datos.
     *
     * @param pokemon Datos a insertar
     */
    public void insertarDatos(PokedexPokemon pokemon) {
        try {
            SQLiteDatabase db = conn.getWritableDatabase();
            ContentValues values = new ContentValues();

            //Insertamos valores
            values.put(CAMPO_POKEMON_ID, pokemon.getId());
            values.put(CAMPO_POKEMON_NOMBRE, pokemon.getNombre());
            values.put(CAMPO_POKEMON_CATEGORIA, pokemon.getCategoria());
            values.put(CAMPO_POKEMON_DESCRIPCION, pokemon.getDescripcion());
            values.put(CAMPO_POKEMON_ALTURA, pokemon.getAltura());
            values.put(CAMPO_POKEMON_PESO, pokemon.getPeso());
            values.put(CAMPO_POKEMON_TIPO_1, pokemon.getTipo1());
            values.put(CAMPO_POKEMON_TIPO_2, pokemon.getTipo2());

            db.insert(NOMBRE_TABLA_POKEMON, CAMPO_POKEMON_ID, values);
            db.close();
        } catch (Exception e) {
            System.out.println("Error insert: " + e.getMessage());
        }
    }

    private class VistaLista {
        private int numero;
        private String nombre;

        public VistaLista(int numero, String nombre) {
            this.numero = numero;
            this.nombre = nombre;
        }

        @Override
        public String toString() {
            return numero + "     " + nombre;
        }
    }
}