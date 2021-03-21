package com.example.mandopokemon.SQL;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

public class ConexionSQLiteHelper extends SQLiteOpenHelper {

    //TABLA POKEMON
    public static final String NOMBRE_TABLA_POKEMON = "POKEMON";
    public static final  String CAMPO_POKEMON_ID = "ID_POKEMON";
    public static final String CAMPO_POKEMON_NOMBRE = "NOMBRE";
    public static final String CAMPO_POKEMON_TIPO_1 = "TIPO_1";
    public static final  String CAMPO_POKEMON_TIPO_2 = "TIPO_2";
    public static final  String CAMPO_POKEMON_DESCRIPCION = "DESCRIPCION";

    //Crear tablas
    private final String CREAR_TABLA_POKEMON = String.format(
            "CREATE TABLE %s(%s INTEGER PRIMARY KEY, %s TEXT NOT NULL, %s INTEGER NOT NULL, %s INTEGER, %s TEXT)",
            NOMBRE_TABLA_POKEMON, CAMPO_POKEMON_ID, CAMPO_POKEMON_NOMBRE, CAMPO_POKEMON_TIPO_1, CAMPO_POKEMON_TIPO_2,
            CAMPO_POKEMON_DESCRIPCION);

    //Drop table
    private final String DROP_TABLA_POKEMON = String.format("DROP TABLE IF EXISTS %s", NOMBRE_TABLA_POKEMON);

    public ConexionSQLiteHelper(Context context, String name, SQLiteDatabase.CursorFactory factory, int version) {
        super(context, name, factory, version);
    }

    @Override
    public void onCreate(SQLiteDatabase db) {
        db.execSQL(CREAR_TABLA_POKEMON);
    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        db.execSQL(DROP_TABLA_POKEMON);
        onCreate(db);
    }
}