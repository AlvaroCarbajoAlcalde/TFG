package com.example.mandopokemon;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

public class MainActivity extends AppCompatActivity {

    private Button btnCombate, btnPokedex;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        btnCombate = findViewById(R.id.btnCombate);
        btnPokedex = findViewById(R.id.btnPokedex);

        btnCombate.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Client.enviarMensaje("START");
                Intent intent = new Intent (v.getContext(), CombateActivity.class);
                startActivityForResult(intent, 0);
            }
        });

        btnPokedex.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent (v.getContext(), PokedexListaActivity.class);
                startActivityForResult(intent, 0);
            }
        });
    }

}