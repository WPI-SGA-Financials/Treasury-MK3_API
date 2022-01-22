package edu.wpi.sga.treasury.application.dto;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class OrganizationDto {
    private String name;
    private String classification;
    private String typeOfClub;
    private String accountNumber;
    private String acronym;
    private Boolean inactive;
}
