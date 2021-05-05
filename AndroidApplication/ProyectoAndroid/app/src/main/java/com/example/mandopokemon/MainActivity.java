package com.example.mandopokemon;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import java.net.Inet4Address;
import java.net.InetAddress;
import java.net.NetworkInterface;
import java.util.Collections;
import java.util.List;
import java.util.Locale;
import java.util.regex.Pattern;

public class MainActivity extends AppCompatActivity {

    private Button btnCombate, btnPokedex;
    private EditText etIP;
    private static final Pattern PATTERN = Pattern.compile("^(([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\.){3}([01]?\\d\\d?|2[0-4]\\d|25[0-5])$");

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        btnCombate = findViewById(R.id.btnCombate);
        btnPokedex = findViewById(R.id.btnPokedex);
        etIP = findViewById(R.id.editTextIP);

        btnCombate.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String ip = etIP.getText().toString();
                System.out.println("IP movil: " + getIP());
                if (esIP(ip)) {
                    Configuracion.IP_PC = ip;
                    Client.enviarMensaje("START" + getIP());
                    Intent intent = new Intent(v.getContext(), CombateActivity.class);
                    startActivityForResult(intent, 0);
                } else {
                    Toast.makeText(getApplicationContext(), "No has introducido una IP válida.", Toast.LENGTH_LONG).show();
                }
            }
        });

        btnPokedex.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(v.getContext(), PokedexListaActivity.class);
                startActivityForResult(intent, 0);
            }
        });
    }

    /**
     * Es ip
     *
     * @param ip ip introducida por el usuario
     * @return true si es una ip valida, false en caso contrario
     */
    private boolean esIP(String ip) {
        return PATTERN.matcher(ip).matches();
    }

    /**
     * Devuelve la IP
     *
     * @return La ip del dispositivo
     */
    public static String getIP() {
        List<InetAddress> addrs;
        String address = "";
        try {
            List<NetworkInterface> interfaces = Collections.list(NetworkInterface.getNetworkInterfaces());
            for (NetworkInterface intf : interfaces) {
                addrs = Collections.list(intf.getInetAddresses());
                for (InetAddress addr : addrs) {
                    if (!addr.isLoopbackAddress() && addr instanceof Inet4Address) {
                        address = addr.getHostAddress().toUpperCase(new Locale("es", "MX"));
                    }
                }
            }
        } catch (Exception e) {
            System.out.println("Error al obtener IP " + e.getMessage());
        }
        return address;
    }

}