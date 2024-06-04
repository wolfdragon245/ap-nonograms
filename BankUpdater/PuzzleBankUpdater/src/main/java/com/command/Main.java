package com.command;

import com.google.gson.Gson;

import javax.swing.*;
import javax.swing.filechooser.FileNameExtensionFilter;
import java.io.*;
import java.util.Scanner;

public class Main {
    public static final String baseURL = "https://commandtm.github.io/ap-nonograms/Puzzles/";
    public static String[] puzzles;

    public static void main(String[] args) throws IOException {
        String path = "";
        String dirPath = "";

        JFileChooser dialog = new JFileChooser();
        FileNameExtensionFilter filter = new FileNameExtensionFilter("index.html", "html");
        dialog.setFileFilter(filter);
        int returnVal = dialog.showOpenDialog(null);
        if (returnVal == JFileChooser.APPROVE_OPTION) {
            path = dialog.getSelectedFile().getAbsolutePath();
        }

        File file = new File(path);
        FileWriter fw = new FileWriter(file);

        dialog.removeChoosableFileFilter(filter);
        dialog.setFileSelectionMode(JFileChooser.DIRECTORIES_ONLY);
        returnVal = dialog.showOpenDialog(null);
        if (returnVal == JFileChooser.APPROVE_OPTION) {
            dirPath = dialog.getSelectedFile().getAbsolutePath();
        }

        File dir = new File(dirPath);
        File[] files = dir.listFiles();
        puzzles = new String[files.length];
        for (int i = 0; i < files.length; i++){
            puzzles[i] = baseURL + files[i].getName();
        }

        for (String puzzle : puzzles){
            System.out.println(puzzle);
        }

        Gson gson = new Gson();
        String data = gson.toJson(puzzles);
        fw.write(data);
        fw.close();
    }
}