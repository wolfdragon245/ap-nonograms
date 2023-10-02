package com.commandtm.bkpicross;

import com.badlogic.gdx.math.MathUtils;

import java.util.Arrays;

public class Board {
    public static boolean[][] board = new boolean[10][10];
    public static boolean[][] flags = new boolean[10][10];
    public static boolean[][] boardSolution;
    public Board(){
        for (boolean[] booleans : board) {
            Arrays.fill(booleans, false);
        }
        boardSolution = new boolean[][]{
                {true, false, false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false, false, false},
        };
        randomizeSolution();
    }

    public static boolean checkBoard(){
        for (int k = 0; k < boardSolution.length; k++){
            for (int i = 0; i < boardSolution[k].length; i++){
                if (board[k][i] != boardSolution[k][i]){
                    return false;
                }
            }
        }
        return true;
    }

    public static void clearBoard(){
        for (boolean[] booleans : board) {
            Arrays.fill(booleans, false);
        }
    }

    public static void clearFlag(){
        for (boolean[] booleans : flags) {
            Arrays.fill(booleans, false);
        }
    }

    public static void randomizeSolution(){
        for (int k = 0; k < boardSolution.length; k++){
            for (int i = 0; i < boardSolution[k].length; i++){
                boardSolution[k][i] = MathUtils.randomBoolean();
            }
        }
    }
}
