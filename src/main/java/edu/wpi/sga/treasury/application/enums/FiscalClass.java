package edu.wpi.sga.treasury.application.enums;

import lombok.Getter;
import lombok.RequiredArgsConstructor;

@RequiredArgsConstructor
@Getter
public enum FiscalClass {
    CLASS_A("Class A"),
    CLASS_B("Class B"),
    CLASS_C("Class C"),
    CLASS_D("Class D"),
    CLASS_E("Class E"),
    CLASS_F("Class F"),
    NOT_BUDGETED("Not Budgeted");

    private final String name;
}
