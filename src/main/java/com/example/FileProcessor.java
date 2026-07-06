package com.example;

import java.io.IOException;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

/* 
    java:S1130 
        Remove unused imports for Exceptions. 
        Propagate the changes to surrounding methods that capture exceptions raised in this method, or that rethrow it.
*/
public class FileProcessor {

    public List<String> readLines(String path) throws IOException, SQLException {
        if (path.isEmpty()) {
            throw new IOException("Path cannot be empty");
        }
        return new ArrayList<>();
    }

    public String summarize(String path) {
        try {
            List<String> lines = readLines(path);
            return "Lines: " + lines.size();
        } catch (IOException | SQLException e) {            // <--------- ISSUE
            return "failed: " + e.getMessage();
        }
    }

    public List<String> loadAndValidate(String path) throws IOException, SQLException {
        List<String> lines = readLines(path);
        if (lines.isEmpty()) {
            throw new IOException("Empty file: " + path);
        }
        return lines;
    }
}
