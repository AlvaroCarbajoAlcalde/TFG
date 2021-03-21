package com.example.mandopokemon;

import android.graphics.drawable.Drawable;
import android.os.Bundle;

import com.example.mandopokemon.Modelo.Movimiento;
import com.example.mandopokemon.Modelo.Pokemon;
import com.example.mandopokemon.Utiles.UtilImagenes;

import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.app.AppCompatDelegate;
import androidx.appcompat.content.res.AppCompatResources;

import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

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

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;

public class CombateActivity extends AppCompatActivity {

    //Variables estaticas
    private static final int POKEMON_FRONT = 0;
    private static final int POKEMON_BACK = 1;
    private static final String RUTA_POKEMON_RECIBIDO = "pokemon_recibido.xml";

    //Variables
    private Pokemon[] pokemonsLuchando;
    private Button btnMov1, btnMov2, btnMov3, btnMov4;
    private TextView tvPokFront, tvPokBack, tvPokFrontVida, tvPokBackVida, txtViewEspera;
    private ImageView ivPokFront, ivPokBack;
    private LinearLayout layoutCombate;
    private Thread hiloServidor;
    private ServerSocket server;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_combate);
        //Para usar vectores
        AppCompatDelegate.setCompatVectorFromResourcesEnabled(true);

        //Inicializamos componentes
        initComponents();

        //Mostramos espera
        layoutCombate.setVisibility(View.GONE);

        //Abrimos servidor
        startServerSocket();
    }

    /**
     * Inicializamos componentes
     */
    private void initComponents() {
        //Botones movimientos
        btnMov1 = findViewById(R.id.btnMov1);
        btnMov2 = findViewById(R.id.btnMov2);
        btnMov3 = findViewById(R.id.btnMov3);
        btnMov4 = findViewById(R.id.btnMov4);

        //Nombres pokemon
        tvPokBack = findViewById(R.id.txtViewNombreBack);
        tvPokFront = findViewById(R.id.txtViewNombreFront);

        //Vidas Pokemon
        tvPokBackVida = findViewById(R.id.txtViewVidaBack);
        tvPokFrontVida = findViewById(R.id.txtViewVidaFront);

        //Imagenes pokemon
        ivPokBack = findViewById(R.id.imagePokemonBack);
        ivPokFront = findViewById(R.id.imagePokemonFront);

        //Texto de espera
        txtViewEspera = findViewById(R.id.txtViewEspera);

        //Listeners Botones ataques
        btnMov1.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Client.enviarMensaje("1");
            }
        });
        btnMov2.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Client.enviarMensaje("2");
            }
        });
        btnMov3.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Client.enviarMensaje("3");
            }
        });
        btnMov4.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Client.enviarMensaje("4");
            }
        });

        //Layout
        layoutCombate = findViewById(R.id.layoutCombate);
    }

    /**
     * Inicia el servidor.
     */
    private void startServerSocket() {

        hiloServidor = new Thread(new Runnable() {
            Socket connection;

            DataInputStream dataInputStream;
            BufferedInputStream bufferedInputStream;
            BufferedOutputStream bufferedOutputStream;

            byte[] receivedData;
            int in;

            @Override
            public void run() {
                try {
                    System.out.println("Servidor Iniciado.");
                    server = new ServerSocket(Configuracion.PUERTO_SERVIDOR);
                    while (true) {
                        //Aceptar conexiones
                        connection = server.accept();

                        //Buffer de 1024 bytes
                        receivedData = new byte[1024];
                        bufferedInputStream = new BufferedInputStream(connection.getInputStream());
                        dataInputStream = new DataInputStream(connection.getInputStream());

                        //Para guardar fichero recibido
                        bufferedOutputStream = new BufferedOutputStream(new FileOutputStream(new File(getExternalFilesDir(null), RUTA_POKEMON_RECIBIDO)));

                        //Leer fichero recibido
                        while ((in = bufferedInputStream.read(receivedData)) != -1) {
                            bufferedOutputStream.write(receivedData, 0, in);
                        }

                        bufferedOutputStream.close();
                        dataInputStream.close();
                        System.out.println("Fichero recibido.");

                        //Se realiza tras recibir el fichero:
                        pokemonsLuchando = leerPokemonsDeXML();

                        //Necesario para que no se produzca excepcion de cambiar vistas desde hilo
                        runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                //Cambio botones
                                setDatosBotones();
                                setDatosPokemons();
                                txtViewEspera.setVisibility(View.GONE);
                                layoutCombate.setVisibility(View.VISIBLE);
                            }
                        });

                    }
                } catch (IOException e) {
                    if (e.getMessage() != "Socket closed")
                        System.out.println("ERROR SERVIDOR TCP: " + e.getMessage());
                }
            }
        });
        hiloServidor.start();
    }

    /**
     * Cambia la vista para colocar los datos de los pokemon
     */
    private void setDatosPokemons() {
        tvPokFront.setText(pokemonsLuchando[POKEMON_FRONT].getNombre());
        tvPokBack.setText(pokemonsLuchando[POKEMON_BACK].getNombre());
        ivPokFront.setImageResource(UtilImagenes.getIdRecursoPokemon(pokemonsLuchando[POKEMON_FRONT].getNumPokedex()));
        ivPokBack.setImageResource(UtilImagenes.getIdRecursoPokemon(pokemonsLuchando[POKEMON_BACK].getNumPokedex()));
        tvPokBackVida.setText(pokemonsLuchando[POKEMON_BACK].getVidaActual() + " / " + pokemonsLuchando[POKEMON_BACK].getVidaMax());
        tvPokFrontVida.setText(pokemonsLuchando[POKEMON_FRONT].getVidaActual() + " / " + pokemonsLuchando[POKEMON_FRONT].getVidaMax());
    }

    /**
     * Lee los datos del XML del pokemon recibido
     *
     * @return Pokemon con los datos leidos del XML
     */
    private Pokemon[] leerPokemonsDeXML() {

        Pokemon[] pokemons = new Pokemon[2];
        Pokemon pokemon = new Pokemon();
        try {
            DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
            DocumentBuilder builder = factory.newDocumentBuilder();
            // Parseamos el documento y lo almacenamos en un objeto Document
            Document doc = builder.parse(new File(getExternalFilesDir(null), RUTA_POKEMON_RECIBIDO));

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
                            case "mov1":
                            case "mov2":
                            case "mov3":
                            case "mov4":
                                //Si es un movimiento leemos sus datos y se lo agregamos al pokemon
                                Movimiento mov = new Movimiento();
                                for (int k = 0; k < nodoActual.getChildNodes().getLength(); k++) {
                                    Node nodoActualMov = nodoActual.getChildNodes().item(k);
                                    if (nodoActualMov.getNodeType() == Node.ELEMENT_NODE) {
                                        switch (nodoActualMov.getNodeName().toLowerCase()) {
                                            case "movid":
                                                mov.setId(Integer.parseInt(nodoActualMov.getChildNodes().item(0).getNodeValue()));
                                                break;
                                            case "movtipo":
                                                mov.setTipo(Integer.parseInt(nodoActualMov.getChildNodes().item(0).getNodeValue()));
                                                break;
                                            case "movnombre":
                                                mov.setNombre(nodoActualMov.getChildNodes().item(0).getNodeValue());
                                                break;
                                            case "pp":
                                                mov.setPp(Integer.parseInt(nodoActualMov.getChildNodes().item(0).getNodeValue()));
                                                break;
                                            case "ppmax":
                                                mov.setPpMax(Integer.parseInt(nodoActualMov.getChildNodes().item(0).getNodeValue()));
                                                break;
                                        }
                                    }
                                }
                                //Vemos que movimiento del pokemon es
                                switch (nodoActual.getNodeName().toLowerCase()) {
                                    case "mov1":
                                        pokemon.setMov1(mov);
                                        break;
                                    case "mov2":
                                        pokemon.setMov2(mov);
                                        break;
                                    case "mov3":
                                        pokemon.setMov3(mov);
                                        break;
                                    case "mov4":
                                        pokemon.setMov4(mov);
                                        break;
                                }
                                break;
                            case "tipo1":
                                pokemon.setTipo1(Integer.parseInt(nodoActual.getChildNodes().item(0).getNodeValue()));
                                break;
                            case "tipo2":
                                pokemon.setTipo2(Integer.parseInt(nodoActual.getChildNodes().item(0).getNodeValue()));
                                break;
                            case "numpokedex":
                                pokemon.setNumPokedex(Integer.parseInt(nodoActual.getChildNodes().item(0).getNodeValue()));
                                break;
                            case "vidamax":
                                pokemon.setVidaMax(Integer.parseInt(nodoActual.getChildNodes().item(0).getNodeValue()));
                                break;
                            case "vidaact":
                                pokemon.setVidaActual(Integer.parseInt(nodoActual.getChildNodes().item(0).getNodeValue()));
                                break;
                            case "estado":
                                pokemon.setEstado(Integer.parseInt(nodoActual.getChildNodes().item(0).getNodeValue()));
                                break;
                        }
                    }
                }
                pokemons[i] = new Pokemon(pokemon);
            }

            return pokemons;

        } catch (ParserConfigurationException | SAXException | IOException e) {
            System.out.println("ERROR al leer XML: " + e.getMessage());
            return null;
        }

    }

    /**
     * Setea los datos de los 4 botones.
     */
    private void setDatosBotones() {
        setDatosBoton(btnMov1, pokemonsLuchando[POKEMON_BACK].getMov1());
        setDatosBoton(btnMov2, pokemonsLuchando[POKEMON_BACK].getMov2());
        setDatosBoton(btnMov3, pokemonsLuchando[POKEMON_BACK].getMov3());
        setDatosBoton(btnMov4, pokemonsLuchando[POKEMON_BACK].getMov4());
    }

    /**
     * Cambia el boton para ajustarse a los datos del movimiento.
     *
     * @param button     Boton del movimiento.
     * @param movimiento Movimiento que usara al accionar el boton.
     */
    private void setDatosBoton(Button button, Movimiento movimiento) {
        //Texto del boton
        button.setText(movimiento.getNombre() + "    " + movimiento.getPp() + " / " + movimiento.getPpMax());

        //Icono del boton
        Drawable myDrawable;
        switch (movimiento.getTipo()) {
            case 1:
                myDrawable = AppCompatResources.getDrawable(this, R.drawable.ic_acero);
                break;
            case 2:
                myDrawable = AppCompatResources.getDrawable(this, R.drawable.ic_agua);
                break;
            case 3:
                myDrawable = AppCompatResources.getDrawable(this, R.drawable.ic_bicho);
                break;
            case 4:
                myDrawable = AppCompatResources.getDrawable(this, R.drawable.ic_dragon);
                break;
            case 5:
                myDrawable = AppCompatResources.getDrawable(this, R.drawable.ic_electrico);
                break;
            case 6:
                myDrawable = AppCompatResources.getDrawable(this, R.drawable.ic_fantasma);
                break;
            case 7:
                myDrawable = AppCompatResources.getDrawable(this, R.drawable.ic_fuego);
                break;
            case 9:
                myDrawable = AppCompatResources.getDrawable(this, R.drawable.ic_hielo);
                break;
            case 10:
                myDrawable = AppCompatResources.getDrawable(this, R.drawable.ic_lucha);
                break;
            case 11:
                myDrawable = AppCompatResources.getDrawable(this, R.drawable.ic_normal);
                break;
            case 12:
                myDrawable = AppCompatResources.getDrawable(this, R.drawable.ic_planta);
                break;
            case 13:
                myDrawable = AppCompatResources.getDrawable(this, R.drawable.ic_psiquico);
                break;
            case 14:
                myDrawable = AppCompatResources.getDrawable(this, R.drawable.ic_roca);
                break;
            case 15:
                myDrawable = AppCompatResources.getDrawable(this, R.drawable.ic_siniestro);
                break;
            case 16:
                myDrawable = AppCompatResources.getDrawable(this, R.drawable.ic_tierra);
                break;
            case 17:
                myDrawable = AppCompatResources.getDrawable(this, R.drawable.ic_veneno);
                break;
            default:
                myDrawable = AppCompatResources.getDrawable(this, R.drawable.ic_volador);
                break;
        }
        myDrawable.setBounds(0, 0, 110, 110);
        button.setCompoundDrawables(myDrawable, null, null, null);
    }

    @Override
    public void onBackPressed() {
        try {
            //Cerramos el servidor al salir
            hiloServidor.interrupt();
            server.close();
            System.out.println("Servidor Cerrado");
            Client.enviarMensaje("STOP");
        } catch (IOException e) {
            e.printStackTrace();
        }
        super.onBackPressed();
    }

}