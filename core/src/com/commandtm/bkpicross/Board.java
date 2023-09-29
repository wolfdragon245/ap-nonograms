package com.commandtm.bkpicross;

import java.util.Arrays;

public class Board {
    public static boolean[][] board = new boolean[10][10];
    public static boolean[][] boardSolution;
    public Board(){
        for (boolean[] booleans : board) {
            Arrays.fill(booleans, false);
        }
        boardSolution = new boolean[][]{
                {true, true, true, true, true, true, true, true, true, true},
                {true, true, true, true, true, true, true, true, true, false},
                {true, true, true, true, true, true, true, true, false, true},
                {true, true, true, true, true, true, true, false, true, true},
                {true, true, true, true, true, true, false, true, false, true},
                {true, true, true, true, true, false, true, true, false, false},
                {true, true, true, true, false, true, false, true, false, true},
                {true, true, true, false, true, true, true, false, true, true},
                {true, true, false, true, false, true, false, true, false, true},
                {false, false, false, false, false, false, false, false, false, false},
        };
    }
}
