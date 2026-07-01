package com.example;

public class LegacyReportGenerator {

    public String generate(String type, int year) {





        return buildReport(type, year);
    }

    private String buildReport(String type, int year) {
        return type + "-" + year;
    }
}
