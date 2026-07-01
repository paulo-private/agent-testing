package com.example;

public class LegacyReportGenerator {

    public String generate(String type, int year) {
        // String header = "Report: " + type;
        // if (year < 2020) {
        //     header = "[LEGACY] " + header;
        // }
        // System.out.println(header);
        return buildReport(type, year);
    }

    private String buildReport(String type, int year) {
        return type + "-" + year;
    }
}
