package com.example.mandopokemon;

import java.io.IOException;
import java.io.OutputStream;
import java.io.PrintWriter;
import java.net.Socket;

public class Client {

    public static void enviarMensaje(final String mensaje) {

        Thread thread = new Thread(new Runnable() {
            @Override
            public void run() {
                try {
                    Socket s = new Socket(Configuracion.IP_PC, Configuracion.PUERTO_ENVIAR);
                    OutputStream out = s.getOutputStream();
                    PrintWriter output = new PrintWriter(out);
                    output.println(mensaje);
                    output.flush();
                    output.close();
                    out.close();
                    s.close();
                } catch (IOException e) {
                    System.out.println("ERROR AL ENVIAR: " + e.getMessage());
                    e.printStackTrace();
                }
            }
        });
        thread.start();
    }
}