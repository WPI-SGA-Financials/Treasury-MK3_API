package edu.wpi.sga.treasury.domain.model;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.math.BigDecimal;
import java.time.LocalDateTime;
import java.util.LinkedHashSet;
import java.util.Set;

@Getter
@Setter
@Entity
@Table(name = "mandatory_transfer_line_item")
public class MandatoryTransferLineItem {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", nullable = false)
    private Integer id;

    @ManyToOne(fetch = FetchType.LAZY, optional = false)
    @JoinColumn(name = "mandatory_transfer_id", nullable = false)
    private MandatoryTransfer mandatoryTransfer;

    @Column(name = "line_item_name", nullable = false)
    private String lineItemName;

    @Column(name = "amount", nullable = false, precision = 10, scale = 2)
    private BigDecimal amount;

    @Column(name = "notes")
    private String notes;

    @Column(name = "last_modified", nullable = false)
    private LocalDateTime lastModified;

    @OneToMany(mappedBy = "mandatoryTransferLineItem")
    private Set<OperatingExpense> operatingExpenses = new LinkedHashSet<>();

}