package com.example.mandopokemon.Modelo;

public class Movimiento {

    private int id;
    private String nombre;
    private int tipo;
    private int pp, ppMax;

    public Movimiento(){

    }

    public Movimiento(int id, String nombre, int tipo){
        this.id = id;
        this.nombre = nombre;
        this.tipo = tipo;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public int getPp() {
        return pp;
    }

    public void setPp(int pp) {
        this.pp = pp;
    }

    public int getPpMax() {
        return ppMax;
    }

    public void setPpMax(int ppMax) {
        this.ppMax = ppMax;
    }

    public String getNombre() {
        return nombre;
    }

    public void setNombre(String nombre) {
        this.nombre = nombre;
    }

    public int getTipo() {
        return tipo;
    }

    public void setTipo(int tipo) {
        this.tipo = tipo;
    }

    @Override
    public String toString() {
        return "Movimiento{" +
                "id=" + id +
                ", nombre='" + nombre + '\'' +
                ", tipo=" + tipo +
                '}';
    }

}
