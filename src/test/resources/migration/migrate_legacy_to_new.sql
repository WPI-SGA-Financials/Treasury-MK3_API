/*
    One time migration script to migrate from the old structure to the new structure
    sgadb - Old DB
    sgadb_new - New DB
*/

insert into sgadb_new.organization (organization_name, classification, type_of_club, account_number, acronym,
                                    is_inactive,
                                    last_modified)
select `Name of Club`, Classification, `Type of Club`, `Account Number`, `Acronym 1`, `Inactive?`, Timestamp
from sgadb.Organizations;

insert into sgadb_new.club_classification (organization_id, category)
select (select sgadb_new.organization.id
        from sgadb_new.organization
        where sgadb.`Club Classifications`.Name = sgadb_new.organization.organization_name) as organization_id,
       Category
from sgadb.`Club Classifications`;

insert into sgadb_new.techsync (organization_id, techsync_name, last_modified)
select (select sgadb_new.organization.id
        from sgadb_new.organization
        where sgadb.`Techsync Names`.`Name of Club` = sgadb_new.organization.organization_name) as organization_id,
       `Techsync Name`,
       Timestamp
from sgadb.`Techsync Names`;

insert into sgadb_new.organization_comment (id, organization_id, comment_date, comment, last_modified)
select ID,
       (select sgadb_new.organization.id
        from sgadb_new.organization
        where sgadb.`Organization Comments`.`Name of Club` =
              sgadb_new.organization.organization_name) as organization_id,
       `Comment Date`,
       Comment,
       Timestamp
from sgadb.`Organization Comments`;

insert into sgadb_new.organization_contact_info (organization_id, president_email, treasurer_email, last_modified)
select (select sgadb_new.organization.id
        from sgadb_new.organization
        where sgadb.`Organizations Contact Info`.`Name of Club` =
              sgadb_new.organization.organization_name) as organization_id,
       `President Email`,
       `Treasurer Email`,
       Timestamp
from sgadb.`Organizations Contact Info`;

insert into sgadb_new.organization_membership_number (organization_id, fiscal_year, amount_member_count, last_modified)
select (select sgadb_new.organization.id
        from sgadb_new.organization
        where sgadb.`Organization Membership Numbers`.`Name of Organization` =
              sgadb_new.organization.organization_name) as organization_id,
       `Fiscal Year`,
       `Active Members`,
       Timestamp
from sgadb.`Organization Membership Numbers`;

insert into sgadb_new.budget (id, organization_id, fiscal_year, notes, last_modified)
select ID,
       (select sgadb_new.organization.id
        from sgadb_new.organization
        where sgadb.Budget.`Name of Club` = sgadb_new.organization.organization_name) as organization_id,
       `Fiscal Year`,
       Notes,
       Timestamp
from sgadb.Budget;

insert into sgadb_new.budget_legacy (id, budget_id, amount_requested, amount_proposed, appealed, amount_appealed,
                                     appeal_decision, approved_appeal, amount_spent, last_modified)
select ID,
       (select sgadb_new.budget.id
        from sgadb_new.budget
        where sgadb.BudgetLegacy.B_ID = sgadb_new.budget.id) as budget_id,
       `Amount Requested`,
       `Amount Proposed`,
       Appealed,
       `Appeal Amount`,
       `Appeal Decision`,
       `Approved Appeal`,
       `Amount Spent`,
       Timestamp
from sgadb.BudgetLegacy;

insert into sgadb_new.budget_section (id, budget_id, section_name, last_modified)
select ID,
       (select sgadb_new.budget.id
        from sgadb_new.budget
        where sgadb.BudgetSection.B_ID = sgadb_new.budget.id) as budget_id,
       Section_Name,
       Timestamp
from sgadb.BudgetSection;

insert into sgadb_new.budget_line_item (id, budget_section_id, line_item_name, amount_requested, amount_proposed,
                                        appealed, amount_appealed, appeal_decision, approved_appeal, amount_spent,
                                        notes, last_modified)
select ID,
       (select sgadb_new.budget_section.id
        from sgadb_new.budget_section
        where sgadb.BudgetLineItem.BS_ID = sgadb_new.budget_section.id) as budget_section_id,
       Line_Item_Name,
       Amount_Request,
       Amount_Proposed,
       Appealed,
       Appeal_Amount,
       Appeal_Decision,
       Approved_Appeal,
       Amount_Spent,
       Notes,
       Timestamp
from sgadb.BudgetLineItem;

insert into sgadb_new.funding_request (id, organization_id, description, hearing_date, fiscal_year, date_of_event,
                                       dot_number, amount_requested, decision, amount_approved, notes, last_modified)
select ID,
       (select sgadb_new.organization.id
        from sgadb_new.organization
        where sgadb.`Funding Requests`.`Name of Club` = sgadb_new.organization.organization_name) as organization_id,
       Description,
       `Funding Date`,
       `Fiscal Year`,
       `Date of Event`,
       `Dot Number`,
       `Amount Requested`,
       Decision,
       `Amount Approved`,
       Notes,
       Timestamp
from sgadb.`Funding Requests`;

insert into sgadb_new.fr_appeal (id, funding_request_id, new_dot_number, appeal_date, description, appeal_amount,
                                 decision, approved_appeal, notes, minutes_link, last_modified)
select ID,
       (select sgadb_new.funding_request.id
        from sgadb_new.funding_request
        where sgadb.FRAppeals.FR_ID = sgadb_new.funding_request.id) as fr_id,
       `New Dot Number`,
       `Appeal Date`,
       Description,
       `Appeal Amount`,
       Decision,
       `Approved Appeal`,
       Notes,
       `Minutes Link`,
       Timestamp
from sgadb.FRAppeals;

insert into sgadb_new.fr_minute (id, funding_request_id, agenda_number, minutes_link, last_modified)
select ID,
       (select sgadb_new.funding_request.id
        from sgadb_new.funding_request
        where sgadb.FRMinutes.FR_ID = sgadb_new.funding_request.id) as fr_id,
       `Agenda Number`,
       `Minutes Link`,
       Timestamp
from sgadb.FRMinutes;

insert into sgadb_new.fr_report_form (id, funding_request_id, amount_spent, status, amount_approved, approved_date,
                                      notes, last_modified)
select ID,
       (select sgadb_new.funding_request.id
        from sgadb_new.funding_request
        where sgadb.FRReportForms.FR_ID = sgadb_new.funding_request.id) as fr_id,
       `Spent Amount`,
       Status,
       `Approved Amount`,
       `Approved Date`,
       Notes,
       Timestamp
from sgadb.FRReportForms;

insert into sgadb_new.fr_supplemental (id, funding_request_id, item_type, other_type, amount_requested, amended,
                                       amended_amount, notes, last_modified)
select ID,
       (select sgadb_new.funding_request.id
        from sgadb_new.funding_request
        where sgadb.FRSupplemental.FR_ID = sgadb_new.funding_request.id) as fr_id,
       `Item Type`,
       `Other Type`,
       `Amount Requested`,
       Amended,
       `Amended Amount`,
       Notes,
       Timestamp
from sgadb.FRSupplemental;

insert into sgadb_new.fr_workday_idt (id, funding_request_id, idt_submitted, workday_approved, workday_approval_date,
                                      last_modified)
select ID,
       (select sgadb_new.funding_request.id
        from sgadb_new.funding_request
        where sgadb.FRWorkdayIDT.FR_ID = sgadb_new.funding_request.id) as fr_id,
       `IDT Submitted`,
       `Workday Approved`,
       `Workday Approval Date`,
       Timestamp
from sgadb.FRWorkdayIDT;

insert into sgadb_new.reallocation (id, organization_id, description, hearing_date, fiscal_year, dot_number,
                                    allocated_from, allocated_to, amount_allocated, decision, amount_approved, notes,
                                    last_modified)
select ID,
       (select sgadb_new.organization.id
        from sgadb_new.organization
        where sgadb.Reallocations.`Name of Club` = sgadb_new.organization.organization_name) as organization_id,
       Description,
       `Hearing Date`,
       `Fiscal Year`,
       `Dot Number`,
       `Allocated From`,
       `Allocated To`,
       `Allocation Amount`,
       Decision,
       `Amount Approved`,
       Notes,
       Timestamp
from sgadb.Reallocations;

insert into sgadb_new.reallocation_minute (id, reallocation_id, agenda_number, minutes_link, last_modified)
select ID,
       (select sgadb_new.reallocation.id
        from sgadb_new.reallocation
        where sgadb.ReallocMinutes.Realloc_ID = sgadb_new.reallocation.id) as reallocation_id,
       `Agenda Number`,
       `Minutes Link`,
       Timestamp
from sgadb.ReallocMinutes;

insert into sgadb_new.reclassification (id, organization_id, hearing_date, fiscal_year, dot_number, original_class,
                                        requested_class, decision, approved_class, notes, last_modified)
select ID,
       (select sgadb_new.organization.id
        from sgadb_new.organization
        where sgadb.Reclassifications.`Name of Club` = sgadb_new.organization.organization_name) as organization_id,
       `Hearing Date`,
       `Fiscal Year`,
       `Dot Number`,
       `Original Class`,
       `Requested Class`,
       Decision,
       `Approved Class`,
       Notes,
       Timestamp
from sgadb.Reclassifications;

insert into sgadb_new.reclassification_minute (id, reclassification_id, agenda_number, minutes_link, last_modified)
select ID,
       (select sgadb_new.reclassification.id
        from sgadb_new.reclassification
        where sgadb.ReclassMinutes.Reclass_ID = sgadb_new.reclassification.id) as reclassification_id,
       `Agenda Number`,
       `Minutes Link`,
       Timestamp
from sgadb.ReclassMinutes;

insert into sgadb_new.mandatory_transfer (id, organization_id, fund_name, fiscal_year, worktag, amount_requested,
                                          amount_proposed, amount_approved, notes, last_modified)
select ID,
       (select sgadb_new.organization.id
        from sgadb_new.organization
        where sgadb.`Mandatory Transfers`.`Parent Organization` =
              sgadb_new.organization.organization_name) as organization_id,
       `Fund Name`,
       `Fiscal Year`,
       Worktag,
       `Amount Requested`,
       `Amount Proposed`,
       `Amount Approved`,
       Notes,
       Timestamp
from sgadb.`Mandatory Transfers`;

insert into sgadb_new.mandatory_transfer_line_item (id, mandatory_transfer_id, line_item_name, amount, notes,
                                                    last_modified)
select ID,
       (select sgadb_new.mandatory_transfer.id
        from sgadb_new.mandatory_transfer
        where sgadb.MTLineItems.MT_ID = sgadb_new.mandatory_transfer.id) as mandatory_transfer_id,
       `Line Item`,
       Amount,
       Notes,
       Timestamp
from sgadb.MTLineItems;

insert into sgadb_new.operating_expense (id, mandatory_transfer_line_item_id, amount_spent, person, description,
                                         payment_type, workday_approved, workday_approval_date, notes, last_modified)
select ID,
       (select sgadb_new.mandatory_transfer_line_item.id
        from sgadb_new.mandatory_transfer_line_item
        where sgadb.`Operating Expenses`.MTLI_ID =
              sgadb_new.mandatory_transfer_line_item.id) as mandatory_transfer_line_item_id,
       Spent,
       Person,
       Description,
       `Payment Type`,
       `Workday Approved`,
       `Workday Approval Date`,
       Notes,
       Timestamp
from sgadb.`Operating Expenses`;

insert into sgadb_new.funding_account (account_name, fiscal_year, fall_transfer, spring_transfer, worktag,
                                       last_modified)
select `Account Name`,
       `Fiscal Year`,
       `Fall Transfer`,
       `Spring Transfer`,
       `Work Day Code`,
       Timestamp
from sgadb.`Funding Accounts`;

insert into sgadb_new.transfer (fiscal_year, funding_account_from, funding_account_to, amount, transfer_date, notes,
                                last_modified)
select `Fiscal Year`,
       (select sgadb_new.funding_account.id
        from sgadb_new.funding_account
        where sgadb.Transfers.`From` = sgadb_new.funding_account.account_name
          and sgadb.Transfers.`Fiscal Year` = sgadb_new.funding_account.fiscal_year) as funding_account_from_id,
       (select sgadb_new.funding_account.id
        from sgadb_new.funding_account
        where sgadb.Transfers.`To` = sgadb_new.funding_account.account_name
          and sgadb.Transfers.`Fiscal Year` = sgadb_new.funding_account.fiscal_year) as funding_account_to_id,
       Amount,
       `Date of Transfer`,
       Notes,
       Timestamp
from sgadb.Transfers;

insert into sgadb_new.student_life_fee (id, fiscal_year, student_life_fee_amount, fall_student_amount, last_modified)
select ID,
       `Fiscal Year`,
       `SLF Amount`,
       `Fall Student Amount`,
       Timestamp
from sgadb.`Student Life Fee`;

insert into sgadb_new.student_organization_council (id, name_of_club, acronym, hearing_date, fiscal_year, type_of_club,
                                                    president_email, treasurer_email, projected_active_members,
                                                    decision, notes, organization_id, last_modified)
select ID,
       `Name of Club`,
       Acronym,
       `Hearing Date`,
       `Fiscal Year`,
       `Type of Club`,
       `President Email`,
       `Treasurer Email`,
       `Projected Active Members`,
       Decision,
       Notes,
       (select sgadb_new.organization.id
        from sgadb_new.organization
        where sgadb.SOC.`Name of Club` = sgadb_new.organization.organization_name) as organization_id,
       Timestamp
from sgadb.SOC;