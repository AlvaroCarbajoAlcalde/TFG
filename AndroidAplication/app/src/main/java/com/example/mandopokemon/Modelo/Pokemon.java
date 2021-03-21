package com.example.mandopokemon.Modelo;

public class Pokemon {

    private int id, numPokedex;
    private String nombre;
    private Movimiento mov1, mov2, mov3, mov4;
    private int tipo1, tipo2;
    private int vidaActual, vidaMax;
    private int estado;

    public Pokemon() {

    }

    public Pokemon(Pokemon pokemon) {
        this.id = pokemon.id;
        this.nombre = pokemon.nombre;
        this.mov1 = pokemon.mov1;
        this.mov2 = pokemon.mov2;
        this.mov3 = pokemon.mov3;
        this.mov4 = pokemon.mov4;
        this.tipo1 = pokemon.tipo1;
        this.tipo2 = pokemon.tipo2;
        this.numPokedex = pokemon.numPokedex;
        this.vidaActual = pokemon.vidaActual;
        this.vidaMax = pokemon.vidaMax;
        this.estado = pokemon.estado;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public int getVidaActual() {
        return vidaActual;
    }

    public void setVidaActual(int vidaActual) {
        this.vidaActual = vidaActual;
    }

    public int getVidaMax() {
        return vidaMax;
    }

    public void setVidaMax(int vidaMax) {
        this.vidaMax = vidaMax;
    }

    public int getEstado() {
        return estado;
    }

    public void setEstado(int estado) {
        this.estado = estado;
    }

    public int getNumPokedex() {
        return numPokedex;
    }

    public void setNumPokedex(int numPokedex) {
        this.numPokedex = numPokedex;
    }

    public String getNombre() {
        return nombre;
    }

    public void setNombre(String nombre) {
        this.nombre = nombre;
    }

    public Movimiento getMov1() {
        return mov1;
    }

    public void setMov1(Movimiento mov1) {
        this.mov1 = mov1;
    }

    public Movimiento getMov2() {
        return mov2;
    }

    public void setMov2(Movimiento mov2) {
        this.mov2 = mov2;
    }

    public Movimiento getMov3() {
        return mov3;
    }

    public void setMov3(Movimiento mov3) {
        this.mov3 = mov3;
    }

    public Movimiento getMov4() {
        return mov4;
    }

    public void setMov4(Movimiento mov4) {
        this.mov4 = mov4;
    }

    public int getTipo1() {
        return tipo1;
    }

    public void setTipo1(int tipo1) {
        this.tipo1 = tipo1;
    }

    public int getTipo2() {
        return tipo2;
    }

    public void setTipo2(int tipo2) {
        this.tipo2 = tipo2;
    }

    @Override
    public String toString() {
        return "Pokemon{" +
                "id=" + id +
                ", nombre='" + nombre + '\'' +
                ", mov1=" + mov1 +
                ", mov2=" + mov2 +
                ", mov3=" + mov3 +
                ", mov4=" + mov4 +
                ", tipo1=" + tipo1 +
                ", tipo2=" + tipo2 +
                '}';
    }

}
