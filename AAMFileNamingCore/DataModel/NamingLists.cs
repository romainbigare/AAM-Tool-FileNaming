using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AAMFileNamingCore.DataModel
{
    public class AbbreviationAttribute : Attribute
    {
        public string Abbreviation { get; }

        public AbbreviationAttribute(string abbreviation)
        {
            Abbreviation = abbreviation;
        }
    }

    public class CategoryAttribute : Attribute
    {
        public string Category { get; }

        public CategoryAttribute(string category)
        {
            Category = category;
        }
    }

    public class ParentCategoryAttribute : Attribute
    {
        public string ParentCategory { get; }

        public ParentCategoryAttribute(string parentCategory)
        {
            ParentCategory = parentCategory;
        }
    }

    public class EnglishNameAttribute : Attribute
    {
        public string EnglishName { get; }

        public EnglishNameAttribute(string englishName)
        {
            EnglishName = englishName;
        }
    }

    public class CustomNamingProperty
    {
        public Type Type { get; set; }

        public string Abbreviation { get; set; }

        public CustomNamingProperty(Type type, string abbreviation)
        {
            Type = type;
            Abbreviation = abbreviation;
        }
    }

    public enum DocumentType
    {
        [Abbreviation("")]
        [EnglishName("None")]
        None, 

        [Abbreviation("L")]
        [EnglishName("Letter")]
        Letter,

        [Abbreviation("A")]
        [EnglishName("Agenda")]
        Agenda,

        [Abbreviation("M")]
        [EnglishName("Minutes")]
        Minutes,

        [Abbreviation("PR")]
        [EnglishName("Publicity")]
        Publicity,
    }

    public enum DocumentTypeIso
    {
        /*/
            DR - All drawings (except Sketches)
            SK - Sketch Drawings
            M3 - All 3D Models
            SH - Schedules
            SP - Specifications

        /*/
        [Abbreviation("")]
        [EnglishName("None")]
        None,
        [Abbreviation("DR")]
        [EnglishName("All drawings (except Sketches)")]
        Drawing,
        [Abbreviation("M3")]
        [EnglishName("All 3D Models")]
        Model3D,
        [Abbreviation("SK")]
        [EnglishName("Sketch Drawings")]
        Sketch,
        [Abbreviation("SH")]
        [EnglishName("Schedules")]
        Schedule,
        [Abbreviation("SP")]
        [EnglishName("Specifications")]
        Specification,
        /*/
        [Abbreviation("AF")]
        [EnglishName("Animation")]
        Animation,
        [Abbreviation("AI")]
        [EnglishName("Architect Instructions")]
        ArchitectsInstruction,
        [Abbreviation("AP")]
        [EnglishName("Appointment")]
        Appointment,
        [Abbreviation("BD")]
        [EnglishName("Bid")]
        Bid,
        [Abbreviation("BR")]
        [EnglishName("Brief")]
        Brief,
        [Abbreviation("CO")]
        [EnglishName("Correspondence")]
        Correspondence,
        [Abbreviation("CT")]
        [EnglishName("Certificate")]
        Certificate,
        [Abbreviation("DI")]
        [EnglishName("Document Issue")]
        DocumentIssue,
        [Abbreviation("DO")]
        [EnglishName("Document")]
        Document,
        [Abbreviation("FN")]
        [EnglishName("File Notes")]
        FileNotes,
        [Abbreviation("HS")]
        [EnglishName("Health and Safety")]
        HealthAndSafety,
        [Abbreviation("IE")]
        [EnglishName("Information Exchange")]
        InformationExchange,
        [Abbreviation("MI")]
        [EnglishName("Minutes")]
        Minutes,
        [Abbreviation("PD")]
        [EnglishName("Project Directory")]
        ProjectDirectory,
        [Abbreviation("PR")]
        [EnglishName("Programme")]
        Programme,
        [Abbreviation("PH")]
        [EnglishName("Photo")]
        Photo,
        [Abbreviation("PP")]
        [EnglishName("Presentation")]
        Presentation,
        [Abbreviation("RD")]
        [EnglishName("Room Data Sheet")]
        RoomDataSheet,
        [Abbreviation("RI")]
        [EnglishName("Request for Information")]
        RequestForInformation,
        [Abbreviation("RM")]
        [EnglishName("Responsibility Matrix")]
        ResponsibilityMatrix,
        [Abbreviation("RP")]
        [EnglishName("Report")]
        Report,
        [Abbreviation("TR")]
        [EnglishName("Tracker")]
        Tracker,
        [Abbreviation("VS")]
        [EnglishName("Visualisation")]
        Visualisation,
        /*/
        [EnglishName("Custom")]
        Custom,
    }

    public enum Level
    {
        [Abbreviation("ZZ")]
        [EnglishName("Multiple Levels")]
        MultipleLevels,
        [Abbreviation("XX")]
        [EnglishName("No Level Applicable")]
        NoLevelApplicable,
        [Abbreviation("B2")]
        [EnglishName("Basement Level 2")]
        BasementLevel2,
        [Abbreviation("B1")]
        [EnglishName("Basement Level 1")]
        BasementLevel1,
        [Abbreviation("LG")]
        [EnglishName("Lower Ground Floor")]
        LowerGroundFloor,
        [Abbreviation("00")]
        [EnglishName("Ground Floor")]
        GroundFloor,
        [Abbreviation("01")]
        [EnglishName("Level 1")]
        Level01,
        [Abbreviation("02")]
        [EnglishName("Level 2")]
        Level02,
        [Abbreviation("M1")]
        [EnglishName("Mezzanine Above Level 1")]
        MezzanineAboveLevel1,
        [Abbreviation("M2")]
        [EnglishName("Mezzanine Above Level 2")]
        MezzanineAboveLevel2,
        [Abbreviation("03")]
        [EnglishName("Level 3")]
        Level03,
        [Abbreviation("04")]
        [EnglishName("Level 4")]
        Level04,
        [Abbreviation("05")]
        [EnglishName("Level 5")]
        Level05,
        [Abbreviation("06")]
        [EnglishName("Level 6")]
        Level06,
        [Abbreviation("07")]
        [EnglishName("Level 7")]
        Level07,
        [Abbreviation("08")]
        [EnglishName("Level 8")]
        Level08,
        [Abbreviation("09")]
        [EnglishName("Level 9")]
        Level09,
        [Abbreviation("10")]
        [EnglishName("Level 10")]
        Level10,
        [Abbreviation("11")]
        [EnglishName("Level 11")]
        Level11,
        [Abbreviation("12")]
        [EnglishName("Level 12")]
        Level12,
        [Abbreviation("13")]
        [EnglishName("Level 13")]
        Level13,
        [Abbreviation("14")]
        [EnglishName("Level 14")]
        Level14,
        [Abbreviation("15")]
        [EnglishName("Level 15")]
        Level15,
        [Abbreviation("16")]
        [EnglishName("Level 16")]
        Level16,
        [Abbreviation("17")]
        [EnglishName("Level 17")]
        Level17,
        [Abbreviation("18")]
        [EnglishName("Level 18")]
        Level18,
        [Abbreviation("19")]
        [EnglishName("Level 19")]
        Level19,
        [Abbreviation("20")]
        [EnglishName("Level 20")]
        Level20,
        [Abbreviation("21")]
        [EnglishName("Level 21")]
        Level21,
        [Abbreviation("22")]
        [EnglishName("Level 22")]
        Level22,
        [Abbreviation("23")]
        [EnglishName("Level 23")]
        Level23,
        [Abbreviation("24")]
        [EnglishName("Level 24")]
        Level24,
        [Abbreviation("25")]

        [EnglishName("Level 25")]
        Level25,
        [Abbreviation("26")]
        [EnglishName("Level 26")]
        Level26,
        [Abbreviation("27")]
        [EnglishName("Level 27")]
        Level27,
        [Abbreviation("28")]
        [EnglishName("Level 28")]
        Level28,
        [Abbreviation("29")]
        [EnglishName("Level 29")]
        Level29,
        [Abbreviation("30")]
        [EnglishName("Level 30")]
        Level30,
        [Abbreviation("31")]
        [EnglishName("Level 31")]
        Level31,
        [Abbreviation("32")]
        [EnglishName("Level 32")]
        Level32,
        [Abbreviation("33")]
        [EnglishName("Level 33")]
        Level33,
        [Abbreviation("34")]
        [EnglishName("Level 34")]
        Level34,
        [Abbreviation("35")]
        [EnglishName("Level 35")]
        Level35,
        [Abbreviation("36")]
        [EnglishName("Level 36")]
        Level36,
        [Abbreviation("37")]
        [EnglishName("Level 37")]
        Level37,
        [Abbreviation("38")]
        [EnglishName("Level 38")]
        Level38,
        [Abbreviation("39")]
        [EnglishName("Level 39")]
        Level39,
        [Abbreviation("40")]
        [EnglishName("Level 40")]
        Level40,
        [Abbreviation("41")]
        [EnglishName("Level 41")]
        Level41,
        [Abbreviation("42")]
        [EnglishName("Level 42")]
        Level42,
        [Abbreviation("43")]
        [EnglishName("Level 43")]
        Level43,
        [Abbreviation("44")]
        [EnglishName("Level 44")]
        Level44,
        [Abbreviation("45")]
        [EnglishName("Level 45")]
        Level45,
        [Abbreviation("46")]
        [EnglishName("Level 46")]
        Level46,
        [Abbreviation("47")]
        [EnglishName("Level 47")]
        Level47,
        [Abbreviation("48")]
        [EnglishName("Level 48")]
        Level48,
        [Abbreviation("49")]
        [EnglishName("Level 49")]
        Level49,
        [Abbreviation("50")]
        [EnglishName("Level 50")]
        Level50,
        [Abbreviation("51")]
        [EnglishName("Level 51")]
        Level51,
        [Abbreviation("52")]
        [EnglishName("Level 52")]
        Level52,
        [Abbreviation("53")]
        [EnglishName("Level 53")]
        Level53,
        [Abbreviation("54")]
        [EnglishName("Level 54")]
        Level54,
        [Abbreviation("55")]
        [EnglishName("Level 55")]
        Level55,

        [EnglishName("Custom")]
        Custom,
    }

    public enum TypeCode
    {
        [Abbreviation("")]
        [Category("General Information")]
        [EnglishName("No type")]
        None,

        [Abbreviation("00")]
        [Category("General Information")]
        [EnglishName("Sketches")]
        Sketches,
        [Abbreviation("01")]
        [Category("General Information")]
        [EnglishName("Site Location Plans / Grid Setting Out / Phasing Drawings")]
        SiteLocationPlansGridSettingOutPhasingDrawings,
        [Abbreviation("03")]
        [Category("General Information")]
        [EnglishName("Schematic Drawings")]
        SchematicDrawings,
        [Abbreviation("04")]
        [Category("General Information")]
        [EnglishName("Scheme Presentation Drawings")]
        SchemePresentationDrawings,
        [Abbreviation("05")]
        [Category("General Information")]
        [EnglishName("As-Existing Drawings & Soft-Strip drawings")]
        AsExistingDrawingsSoftStripDrawings,
        [Abbreviation("06")]
        [Category("General Information")]
        [EnglishName("General Arrangement and Assembly Drawings")]
        GeneralArrangementAndAssemblyDrawings,
        [Abbreviation("07")]
        [Category("General Information")]
        [EnglishName("Planning Application Drawings")]
        PlanningApplicationDrawings,
        [Abbreviation("08")]
        [Category("General Information")]
        [EnglishName("Fire and Building Regulation Drawings")]
        FireAndBuildingRegulationDrawings,
        [Abbreviation("11")]
        [Category("Ground, Sub-Structures")]
        [EnglishName("Excavations")]
        Excavations,
        [Abbreviation("13")]
        [Category("Ground, Sub-Structures")]
        [EnglishName("Floor beds")]
        FloorBeds,
        [Abbreviation("16")]
        [Category("Ground, Sub-Structures")]
        [EnglishName("Foundations")]
        Foundations,
        [Abbreviation("17")]
        [Category("Ground, Sub-Structures")]
        [EnglishName("Pile Foundations")]
        PileFoundations,
        [Abbreviation("21")]
        [Category("Primary Elements / Carcass")]
        [EnglishName("External Envelope Systems (EES)")]
        ExternalEnvelopeSystemsEES,
        [Abbreviation("22")]
        [Category("Primary Elements / Carcass")]
        [EnglishName("Internal Linings and Partitions (ILP)")]
        InternalLiningsAndPartitionsILP,
        [Abbreviation("24")]
        [Category("Primary Elements / Carcass")]
        [EnglishName("Stairs, Ramps and Lift Shafts (Assembly Drawings)")]
        StairsRampsAndLiftShaftsAssemblyDrawings,
        [Abbreviation("27")]
        [Category("Primary Elements / Carcass")]
        [EnglishName("Roofs (RFS)")]
        RoofsRFS,
        [Abbreviation("32")]
        [Category("Secondary Elements (if drawn separately from 2-)")]
        [EnglishName("Internal Doors (IDR) and Joinery (JNY)")]
        InternalDoorsIDRAndJoineryJNY,
        [Abbreviation("33")]
        [Category("Secondary Elements (if drawn separately from 2-)")]
        [EnglishName("Floor Systems (FLS), Screeds (SCR) and Floor Finishes (FIN)")]
        FloorSystemsFLSScreedsSCRAndFloorFinishesFIN,
        [Abbreviation("34")]
        [Category("Secondary Elements (if drawn separately from 2-)")]
        [EnglishName("Architectural Metalwork (AMT)")]
        ArchitecturalMetalworkAMT,
        [Abbreviation("35")]
        [Category("Secondary Elements (if drawn separately from 2-)")]
        [EnglishName("Ceilings (CLG)")]
        CeilingsCLG,
        [Abbreviation("42")]
        [Category("Finishes (if drawn separately)")]
        [EnglishName("All other site-applied finishes, including finishes schedule.")]
        AllOtherSiteAppliedFinishesIncludingFinishesSchedule,
        [Abbreviation("43")]
        [Category("Finishes (if drawn separately)")]
        [EnglishName("Floor Finishes (FIN)")]
        FloorFinishesFIN,
        [Abbreviation("70")]
        [Category("Fittings & Fixtures")]
        [EnglishName("Signage (SGN)")]
        SignageSGN,
        [Abbreviation("72")]
        [Category("Fittings & Fixtures")]
        [EnglishName("Fixed Furniture and Equipment (FFE)")]
        FixedFurnitureAndEquipmentFFE,
        [Abbreviation("73")]
        [Category("Fittings & Fixtures")]
        [EnglishName("Kitchens (KIT)")]
        KitchensKIT,
        [Abbreviation("74")]
        [Category("Fittings & Fixtures")]
        [EnglishName("Sanitary (SAN) and Bathroom Pods (POD)")]
        SanitarySANAndBathroomPodsPOD,
        [Abbreviation("75")]
        [Category("Fittings & Fixtures")]
        [EnglishName("Maintenance Access Equipment (MAE) - e.g. Work Restraint Lines")]
        MaintenanceAccessEquipmentMAE,
        [Abbreviation("80")]
        [Category("Loose Furniture and Equipment")]
        [EnglishName("Loose Furniture and Equipment")]
        LooseFurnitureAndEquipment,
        [Abbreviation("90")]
        [Category("External")]
        [EnglishName("External Works (EXW)")]
        ExternalWorksEXW,

        [EnglishName("Custom")]
        Custom,
    }

    public enum FileCode
    {
        [Abbreviation("")]
        [Category("None")]
        [EnglishName("None")]
        None,

        [ParentCategory("Export / Import")]
        [Abbreviation("SK")]
        [EnglishName("Sketch (can be a CAD file, hand sketch, diagram, or other)")]
        Sketch,

        [ParentCategory("Export / Import")]
        [Abbreviation("PR")]
        [EnglishName("Press Release")]
        PressRelease,

        [ParentCategory("Export / Import")]
        [Abbreviation("TR")]
        [EnglishName("Tracking Log")]
        TrackingLog,

        [ParentCategory("Export / Import")]
        [Abbreviation("DIR")]
        [EnglishName("Document Issue Register")]
        DocumentIssueRegister,

        [ParentCategory("Export / Import")]
        [Abbreviation("DIS")]
        [EnglishName("Document Issue Sheet")]
        DocumentIssueSheet,

        [ParentCategory("Graphics")]
        [Abbreviation("R")]
        [EnglishName("Report to be printed or read on screen")]
        Report,

        [ParentCategory("Graphics")]
        [Abbreviation("P")]
        [EnglishName("Presentation, Slides for a meeting or talk. Also includes Videos.")]
        Presentation,

        [ParentCategory("Admin")]
        [Abbreviation("00X")]
        [Category("Documents for Internal Circulation")]
        [EnglishName("Documents created for Internal Circulation")]
        DocumentForInternalCirculation,

        // Documents related to the CLIENT
        [ParentCategory("Admin")]
        [Abbreviation("01A")]
        [Category("Documents related to the CLIENT")]
        [EnglishName("Documents related to the Clients Agent")]
        ClientAgent,


        [ParentCategory("Admin")]
        [Abbreviation("01B")]
        [Category("Documents related to the CLIENT")]
        [EnglishName("Client brief and related documents")]
        ClientBrief,

        [ParentCategory("Admin")]
        [Abbreviation("01C")]
        [Category("Documents related to the CLIENT")]
        [EnglishName("Client Correspondence (except fee letters)")]
        ClientCorrespondence,

        [ParentCategory("Admin")]
        [Abbreviation("01CR")]
        [Category("Documents related to the CLIENT")]
        [EnglishName("Client Change Request")]
        ClientChangeRequest,

        [ParentCategory("Admin")]
        [Abbreviation("01D")]
        [Category("Documents related to the CLIENT")]
        [EnglishName("Other Client documents")]
        OtherClientDocuments,

        [ParentCategory("Admin")]
        [Abbreviation("01F")]
        [Category("Documents related to the CLIENT")]
        [EnglishName("Fee letters, appointments and other related documents")]
        FeeLettersAndAppointments,

        // Documents related to the DESIGN
        [ParentCategory("Admin")]
        [Abbreviation("02DEL")]
        [Category("Documents related to the DESIGN")]
        [EnglishName("Project Deliverables")]
        ProjectDeliverables,

        [ParentCategory("Admin")]
        [Abbreviation("02DN")]
        [Category("Documents related to the DESIGN")]
        [EnglishName("Design Notes")]
        DesignNotes,

        [ParentCategory("Admin")]
        [Abbreviation("02PR")]
        [Category("Documents related to the DESIGN")]
        [EnglishName("Programmes")]
        Programmes,

        [ParentCategory("Admin")]
        [Abbreviation("02R")]
        [Category("Documents related to the DESIGN")]
        [EnglishName("Design Risks / Risk Registers")]
        DesignRisks,

        [ParentCategory("Admin")]
        [Abbreviation("02RE")]
        [Category("Documents related to the DESIGN")]
        [EnglishName("Research")]
        Research,

        [ParentCategory("Admin")]
        [Abbreviation("02REP")]
        [Category("Documents related to the DESIGN")]
        [EnglishName("Reports")]
        Reports,

        [ParentCategory("Admin")] [Abbreviation("02QA")]
        [Category("Documents related to the DESIGN")]
        [EnglishName("All Schedules (Areas, Doors, etc).")]
        Schedules,

        [ParentCategory("Admin")] [Abbreviation("02RF")]
        [Category("Documents related to the DESIGN")]
        [EnglishName("Reference or Precedent Documents and Images")]
        ReferenceDocuments,

        [ParentCategory("Admin")] [Abbreviation("02SI")]
        [Category("Documents related to the DESIGN")]
        [EnglishName("Documents and Images related to the Site / Site Visits.")]
        SiteDocuments,

        // Documents related to COMPLIANCE
        [ParentCategory("Admin")] [Abbreviation("03BR")]
        [Category("Documents related to COMPLIANCE")]
        [EnglishName("Documents related to Building Regulations")]
        BuildingRegulations,

        [ParentCategory("Admin")] [Abbreviation("03EA")]
        [Category("Documents related to COMPLIANCE")]
        [EnglishName("Documents related to Environmental Agency Compliance")]
        EnvironmentalAgencyCompliance,

        [ParentCategory("Admin")] [Abbreviation("03EI")]
        [Category("Documents related to COMPLIANCE")]
        [EnglishName("Documents related to Environmental Impact Compliance")]
        EnvironmentalImpactCompliance,

        [ParentCategory("Admin")] [Abbreviation("03LDA")]
        [Category("Documents related to COMPLIANCE")]
        [EnglishName("Documents related to London Development Agency Compliance")]
        LondonDevelopmentAgencyCompliance,

        [ParentCategory("Admin")] [Abbreviation("03HS")]
        [Category("Documents related to COMPLIANCE")]
        [EnglishName("CDM - Health and Safety")]
        HealthAndSafety,

        [ParentCategory("Admin")] [Abbreviation("03PC")]
        [Category("Documents related to COMPLIANCE")]
        [EnglishName("Documents related to Public Consultations")]
        PublicConsultations,

        [ParentCategory("Admin")] [Abbreviation("03PD")]
        [Category("Documents related to COMPLIANCE")]
        [EnglishName("CDM - Principal Designer")]
        PrincipalDesigner,

        [ParentCategory("Admin")] [Abbreviation("03PL")]
        [Category("Documents related to COMPLIANCE")]
        [EnglishName("Documents related to Planning Compliance")]
        PlanningCompliance,

        [ParentCategory("Admin")] [Abbreviation("03PW")]
        [Category("Documents related to COMPLIANCE")]
        [EnglishName("Documents related to Party Wall Compliance")]
        PartyWallCompliance,

        [ParentCategory("Admin")] [Abbreviation("03RL")]
        [Category("Documents related to COMPLIANCE")]
        [EnglishName("Documents related to Rights To Light Compliance")]
        RightsToLightCompliance,

        [ParentCategory("Admin")] [Abbreviation("03RP")]
        [Category("Documents related to COMPLIANCE")]
        [EnglishName("Documents related to Royal Parks Authority Compliance")]
        RoyalParksAuthorityCompliance,

        [ParentCategory("Admin")] [Abbreviation("03TP")]
        [Category("Documents related to COMPLIANCE")]
        [EnglishName("Documents related to Tree Protection Compliance")]
        TreeProtectionCompliance,

        // Document related to the QUANTITY SURVEYOR / COSTS
        [ParentCategory("Admin")] [Abbreviation("04C")]
        [Category("Document related to the QUANTITY SURVEYOR / COSTS")]
        [EnglishName("Letters to Cost Consultant / Quantity Surveyor")]
        LettersToCostConsultant,

        [ParentCategory("Admin")] [Abbreviation("04QS")]
        [Category("Document related to the QUANTITY SURVEYOR / COSTS")]
        [EnglishName("Documents related to Costs / Quantities")]
        CostsAndQuantities,

        [ParentCategory("Admin")] [Abbreviation("04M")]
        [Category("Document related to the QUANTITY SURVEYOR / COSTS")]
        [EnglishName("Meetings related to Costs / Quantities.")]
        CostMeetings,

        // Documents related to the STRUCTURAL DESIGN or ENGINEER
        [ParentCategory("Admin")] [Abbreviation("05C")]
        [Category("Documents related to the STRUCTURAL DESIGN or ENGINEER")]
        [EnglishName("Letters to the Structural Engineer")]
        LettersToStructuralEngineer,

        [ParentCategory("Admin")] [Abbreviation("05SE")]
        [Category("Documents related to the STRUCTURAL DESIGN or ENGINEER")]
        [EnglishName("Documents related to Structural Engineering")]
        StructuralEngineering,

        // Document related to the SERVICES DESIGN or ENGINEER
        [ParentCategory("Admin")] [Abbreviation("06C")]
        [Category("Document related to the SERVICES DESIGN or ENGINEER")]
        [EnglishName("Letters to the Services Engineer")]
        LettersToServicesEngineer,

        [ParentCategory("Admin")] [Abbreviation("06MEP")]
        [Category("Document related to the SERVICES DESIGN or ENGINEER")]
        [EnglishName("Documents related to Services Engineering")]
        ServicesEngineering,

        // Documents related to SPECIALISTS and SPECIALIST AREAS
        [ParentCategory("Admin")] [Abbreviation("07106")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Section 106 Agreement / Consultant")]
        Section106,

        [ParentCategory("Admin")] [Abbreviation("07AA")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Adjacent Site Architects")]
        AdjacentSiteArchitects,

        [ParentCategory("Admin")] [Abbreviation("07AC")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Acoustic Design / Consultant")]
        AcousticDesign,

        [ParentCategory("Admin")] [Abbreviation("07ACC")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Access Design / Consultant")]
        AccessDesign,

        [ParentCategory("Admin")] [Abbreviation("07AO")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Adjoining Owners Agent")]
        AdjoiningOwnersAgent,

        [ParentCategory("Admin")] [Abbreviation("07AR")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Art Consultant")]
        ArtConsultant,

        [ParentCategory("Admin")] [Abbreviation("07ARB")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Tree Specialist")]
        TreeSpecialist,

        [ParentCategory("Admin")] [Abbreviation("07ARC")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to Archaeology / the Archaeologist")]
        Archaeology,

        [ParentCategory("Admin")] [Abbreviation("07AS")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Asbestos Strategy / Surveyor")]
        AsbestosStrategy,

        [ParentCategory("Admin")] [Abbreviation("07AV")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Audio Visual Design / Consultant")]
        AudioVisualDesign,

        [ParentCategory("Admin")] [Abbreviation("07BIM")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to BIM or BIM Managers / Consultants")]
        BIM,

        [ParentCategory("Admin")] [Abbreviation("07BR")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to BREEAM / BREEAM Consultants")]
        BREEAM,

        [ParentCategory("Admin")] [Abbreviation("07C20")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Twentieth Century Society")]
        TwentiethCenturySociety,

        [ParentCategory("Admin")] [Abbreviation("07CA")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Catering Strategy / Specialist")]
        CateringStrategy,

        [ParentCategory("Admin")] [Abbreviation("07CE")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Civil Design / Engineer")]
        CivilDesign,

        [ParentCategory("Admin")] [Abbreviation("07CL")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Cladding Consultant")]
        CladdingConsultant,

        [ParentCategory("Admin")] [Abbreviation("07CLP")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Clinic Planner")]
        ClinicPlanner,

        [ParentCategory("Admin")] [Abbreviation("07CM")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Computer Modeller")]
        ComputerModeller,

        [ParentCategory("Admin")] [Abbreviation("07CMM")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Comms. Design / Consultant")]
        CommsDesign,

        [ParentCategory("Admin")] [Abbreviation("07CO")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Conservation Strategy / Specialist")]
        ConservationStrategy,

        [ParentCategory("Admin")] [Abbreviation("07CP")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Construction Programmer")]
        ConstructionProgrammer,

        [ParentCategory("Admin")] [Abbreviation("07CS")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to Building Conditions / the Conditions Surveyor")]
        BuildingConditions,

        [ParentCategory("Admin")] [Abbreviation("07CT")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to Coatings / the Coatings Specialist")]
        Coatings,

        [ParentCategory("Admin")] [Abbreviation("07CW")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Clerk Of Works")]
        ClerkOfWorks,

        [ParentCategory("Admin")] [Abbreviation("07DR")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Drainage Strategy / Specialist")]
        DrainageStrategy,

        [ParentCategory("Admin")] [Abbreviation("07DS")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to Daylight Sunlight / Specialist")]
        DaylightSunlight,

        [ParentCategory("Admin")] [Abbreviation("07EC")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Environmental Strategy / Consultant")]
        EnvironmentalStrategy,

        [ParentCategory("Admin")] [Abbreviation("07EL")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Electrical Services Design / Consultant")]
        ElectricalServicesDesign,

        [ParentCategory("Admin")] [Abbreviation("07EOD")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Explosive Ordnance Disposal")]
        ExplosiveOrdnanceDisposal,

        [ParentCategory("Admin")] [Abbreviation("07EX")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Exhibition Design / Designer")]
        ExhibitionDesign,

        [ParentCategory("Admin")] [Abbreviation("07FA")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Facade Design / Specialist")]
        FacadeDesign,

        [ParentCategory("Admin")] [Abbreviation("07FL")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Flood Design / Consultant")]
        FloodDesign,

        [ParentCategory("Admin")] [Abbreviation("07FO")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Fit-Out Design / Specialist")]
        FitOutDesign,

        [ParentCategory("Admin")] [Abbreviation("07FR")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Fire Strategy / Consultant")]
        FireStrategy,

        [ParentCategory("Admin")] [Abbreviation("07FRA")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Flood Risk Strategy / Assessor")]
        FloodRiskStrategy,

        [ParentCategory("Admin")] [Abbreviation("07FU")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to Funding / the Funding Agent")]
        Funding,

        [ParentCategory("Admin")] [Abbreviation("07GD")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Graphic Designer")]
        GraphicDesigner,

        [ParentCategory("Admin")] [Abbreviation("07GS")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Geotechnical Design / Specialist")]
        GeotechnicalDesign,

        [ParentCategory("Admin")] [Abbreviation("07HI")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Historian")]
        Historian,

        [ParentCategory("Admin")] [Abbreviation("07HO")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Hospitality Strategy / Consultants")]
        HospitalityStrategy,

        [ParentCategory("Admin")] [Abbreviation("07HP")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Healthcare Strategy / Planner")]
        HealthcareStrategy,

        [ParentCategory("Admin")] [Abbreviation("07HT")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Hotel Design / Specialists")]
        HotelDesign,

        [ParentCategory("Admin")] [Abbreviation("07HW")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Highways Strategy / Consultant")]
        HighwaysStrategy,

        [ParentCategory("Admin")] [Abbreviation("07HZ")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Hazardous Materials / Surveyor")]
        HazardousMaterials,

        [ParentCategory("Admin")] [Abbreviation("07IB")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to Insurance / the Insurance Broker")]
        Insurance,

        [ParentCategory("Admin")] [Abbreviation("07ID")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Interior Design / Designer")]
        InteriorDesign,

        [ParentCategory("Admin")] [Abbreviation("07INT")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Interpreter - Translator")]
        Interpreter,

        [ParentCategory("Admin")] [Abbreviation("07IT")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the IT Strategy / Specialist")]
        ITStrategy,

        [ParentCategory("Admin")] [Abbreviation("07KT")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Kitchen Design / Specialist")]
        KitchenDesign,

        [ParentCategory("Admin")] [Abbreviation("07LA")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Landscape Design / Architect")]
        LandscapeDesign,

        [ParentCategory("Admin")] [Abbreviation("07LF")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Lift Strategy / Consultant")]
        LiftStrategy,

        [ParentCategory("Admin")] [Abbreviation("07LG")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Local Interest Groups")]
        LocalInterestGroups,

        [ParentCategory("Admin")] [Abbreviation("07LI")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Lighting Design / Consultant")]
        LightingDesign,

        [ParentCategory("Admin")] [Abbreviation("07LM")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Landscape Manager")]
        LandscapeManager,

        [ParentCategory("Admin")] [Abbreviation("07LO")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Landowner")]
        Landowner,

        [ParentCategory("Admin")] [Abbreviation("07LR")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Local Residents")]
        LocalResidents,

        [ParentCategory("Admin")] [Abbreviation("07LW")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Legal Agent")]
        LegalAgent,

        [ParentCategory("Admin")] [Abbreviation("07MA")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Maintenance + Acccess Strategy / Specialist")]
        MaintenanceAccessStrategy,

        [ParentCategory("Admin")] [Abbreviation("07MA")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Masterplanners")]
        Masterplanners,

        [ParentCategory("Admin")] [Abbreviation("07MAS")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Masonry Design / Specialist")]
        MasonryDesign,

        [ParentCategory("Admin")] [Abbreviation("07MET")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Metallurgist")]
        Metallurgist,

        [ParentCategory("Admin")] [Abbreviation("07MM")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to Modelmaking / the Modelmakers")]
        Modelmaking,

        [ParentCategory("Admin")] [Abbreviation("07MP")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Model Photographer")]
        ModelPhotographer,

        [ParentCategory("Admin")] [Abbreviation("07MT")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Model Transporter")]
        ModelTransporter,

        [ParentCategory("Admin")] [Abbreviation("07OA")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Office Agent")]
        OfficeAgent,

        [ParentCategory("Admin")] [Abbreviation("07OM")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the O&M Manual Specialist")]
        OMManualSpecialist,

        [ParentCategory("Admin")] [Abbreviation("07PA")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Perspective Artist")]
        PerspectiveArtist,

        [ParentCategory("Admin")] [Abbreviation("07PF")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to People Flow (Crowd Planning) / Consultant")]
        PeopleFlow,

        [ParentCategory("Admin")] [Abbreviation("07PH")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to Photography / the Photographer")]
        Photography,

        [ParentCategory("Admin")] [Abbreviation("07PHC")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Public Health Strategy / Consultant")]
        PublicHealthStrategy,

        [ParentCategory("Admin")] [Abbreviation("07PL")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to Planning / the Planning Consultant")]
        Planning,

        [ParentCategory("Admin")] [Abbreviation("07PR")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to Public Relations")]
        PublicRelations,

        [ParentCategory("Admin")] [Abbreviation("07PRC")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Property Consultant")]
        PropertyConsultant,

        [ParentCategory("Admin")] [Abbreviation("07PRM")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Property Manager")]
        PropertyManager,

        [ParentCategory("Admin")] [Abbreviation("07PV")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to Photovoltaics")]
        Photovoltaics,

        [ParentCategory("Admin")] [Abbreviation("07PW")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Party Wall Strategy / Surveyor")]
        PartyWallStrategy,

        [ParentCategory("Admin")] [Abbreviation("07RA")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Retail Agent")]
        RetailAgent,

        [ParentCategory("Admin")] [Abbreviation("07RC")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Retail Consultant")]
        RetailConsultant,

        [ParentCategory("Admin")] [Abbreviation("07RESA")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Residential Agent")]
        ResidentialAgent,

        [ParentCategory("Admin")] [Abbreviation("07RI")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Risk Consultant")]
        RiskConsultant,

        [ParentCategory("Admin")] [Abbreviation("07RL")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Rights To Light Surveyor")]
        RightsToLightSurveyor,

        [ParentCategory("Admin")] [Abbreviation("07SE")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Security Strategy / Consultant")]
        SecurityStrategy,

        [ParentCategory("Admin")] [Abbreviation("07SG")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Solar Glare Specialist")]
        SolarGlareSpecialist,

        [ParentCategory("Admin")] [Abbreviation("07SP")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to Space Planning / the Space Planners")]
        SpacePlanning,

        [ParentCategory("Admin")] [Abbreviation("07ST")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Studio Specialist")]
        StudioSpecialist,

        [ParentCategory("Admin")] [Abbreviation("07SU")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to Surveys / the Surveyor")]
        Surveys,

        [ParentCategory("Admin")] [Abbreviation("07SUS")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Sustainability Strategy / Consultant")]
        SustainabilityStrategy,

        [ParentCategory("Admin")] [Abbreviation("07TH")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Theatre Design / Consultant")]
        TheatreDesign,

        [ParentCategory("Admin")] [Abbreviation("07TN")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Townscape Strategy / Consultant")]
        TownscapeStrategy,

        [ParentCategory("Admin")] [Abbreviation("07TR")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Traffic or Transport Strategy / Consultant")]
        TransportStrategy,

        [ParentCategory("Admin")] [Abbreviation("07UD")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Urban Designer")]
        UrbanDesigner,

        [ParentCategory("Admin")] [Abbreviation("07VIS")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to Visualisation / the Visualisation Consultant")]
        Visualisation,

        [ParentCategory("Admin")] [Abbreviation("07WC")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Wind Strategy / Consultant")]
        WindStrategy,

        [ParentCategory("Admin")] [Abbreviation("07WF")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Wayfinding Design / Consultant")]
        WayfindingDesign,

        [ParentCategory("Admin")] [Abbreviation("07WR")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Waste And Recycling Strategy / Specialist")]
        WasteAndRecyclingStrategy,

        [ParentCategory("Admin")] [Abbreviation("07XA")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Executive Architect")]
        ExecutiveArchitect,

        // Documents related to the Design / Project TEAM
        [ParentCategory("Admin")] [Abbreviation("08C")]
        [Category("Documents related to the Design / Project TEAM")]
        [EnglishName("Letters to the Design / Project Team")]
        LettersToDesignTeam,

        [ParentCategory("Admin")] [Abbreviation("08CA")]
        [Category("Documents related to the Design / Project TEAM")]
        [EnglishName("Consultants Appointments and related Documents")]
        ConsultantAppointments,

        [ParentCategory("Admin")] [Abbreviation("08CC")]
        [Category("Documents related to the Design / Project TEAM")]
        [EnglishName("Documents related to Change Control")]
        ChangeControl,

        [ParentCategory("Admin")] [Abbreviation("08DT")]
        [Category("Documents related to the Design / Project TEAM")]
        [EnglishName("Documents related to the Design Team")]
        DesignTeam,

        [ParentCategory("Admin")] [Abbreviation("08MD")]
        [Category("Documents related to the Design / Project TEAM")]
        [EnglishName("Design Team Meetings (e.g Agendas, Minutes)")]
        DesignTeamMeetings,

        [ParentCategory("Admin")] [Abbreviation("08PD")]
        [Category("Documents related to the Design / Project TEAM")]
        [EnglishName("Project Directory and related Documents")]
        ProjectDirectory,

        [ParentCategory("Admin")] [Abbreviation("08PM")]
        [Category("Documents related to the Design / Project TEAM")]
        [EnglishName("Documents related to the Project Management")]
        ProjectManagement,

        [ParentCategory("Admin")] [Abbreviation("08PR")]
        [Category("Documents related to the Design / Project TEAM")]
        [EnglishName("Team Programme and related Documents")]
        TeamProgramme,

        [ParentCategory("Admin")] [Abbreviation("08MP")]
        [Category("Documents related to the Design / Project TEAM")]
        [EnglishName("Project Team Meetings (e.g Agendas, Minutes)")]
        ProjectTeamMeetings,

        [ParentCategory("Admin")] [Abbreviation("08PT")]
        [Category("Documents related to the Design / Project TEAM")]
        [EnglishName("Documents related to the Project Team")]
        ProjectTeam,

        [ParentCategory("Admin")] [Abbreviation("08RM")]
        [Category("Documents related to the Design / Project TEAM")]
        [EnglishName("Responsibility Matrix and related Documents")]
        ResponsibilityMatrix,

        [ParentCategory("Admin")] [Abbreviation("08SC")]
        [Category("Documents related to the Design / Project TEAM")]
        [EnglishName("Documents related to Subconsultants")]
        Subconsultants,

        [ParentCategory("Admin")] [Abbreviation("08VE")]
        [Category("Documents related to the Design / Project TEAM")]
        [EnglishName("Documents related to Value Engineering")]
        ValueEngineering,

        // Documents related to the TECHNICAL Design
        [ParentCategory("Admin")] [Abbreviation("09AMT")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the AMT package")]
        AMTPackage,

        [ParentCategory("Admin")] [Abbreviation("09CLG")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the CLG package")]
        CLGPackage,

        [ParentCategory("Admin")] [Abbreviation("09EES")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the EES package")]
        EESPackage,

        [ParentCategory("Admin")] [Abbreviation("09EXW")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the EXW package")]
        EXWPackage,

        [ParentCategory("Admin")] [Abbreviation("09FFE")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the FFE package")]
        FFEPackage,

        [ParentCategory("Admin")] [Abbreviation("09FIN")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the FIN package")]
        FINPackage,

        [ParentCategory("Admin")] [Abbreviation("09FLS")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the FLS package")]
        FLSPackage,

        [ParentCategory("Admin")] [Abbreviation("09FPS")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the FPS package")]
        FPSPackage,

        [ParentCategory("Admin")] [Abbreviation("09IDR")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the IDR package")]
        IDRPackage,

        [ParentCategory("Admin")] [Abbreviation("09ILP")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the ILP package")]
        ILPPackage,

        [ParentCategory("Admin")] [Abbreviation("09IPS")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the IPS package")]
        IPSPackage,

        [ParentCategory("Admin")] [Abbreviation("09ITW")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the ITW package")]
        ITWPackage,

        [ParentCategory("Admin")] [Abbreviation("09JNY")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the JNY package")]
        JNYPackage,

        [ParentCategory("Admin")] [Abbreviation("09KIT")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the KIT package")]
        KITPackage,

        [ParentCategory("Admin")] [Abbreviation("09MAE")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the MAE package")]
        MAEPackage,

        [ParentCategory("Admin")] [Abbreviation("09RFS")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the RFS package")]
        RFSPackage,

        [ParentCategory("Admin")] [Abbreviation("09SAN")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the SAN package")]
        SANPackage,

        [ParentCategory("Admin")] [Abbreviation("09SCR")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the SCR package")]
        SCRPackage,

        [ParentCategory("Admin")] [Abbreviation("09SGN")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the SGN package")]
        SGNPackage,

        [ParentCategory("Admin")] [Abbreviation("09TD")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents created by the project Technical Director")]
        TechnicalDirector,

        [ParentCategory("Admin")] [Abbreviation("09USD")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the USD package")]
        USDPackage,

        // 12 - Documents related to the MAIN CONTRACTOR
        [ParentCategory("Admin")]
        [Abbreviation("12C")]
        [Category("Documents related to the MAIN CONTRACTOR")]
        [EnglishName("Letters to a Main Contractor")]
        LettersToMainContractor,

        [ParentCategory("Admin")]
        [Abbreviation("12SR")]
        [Category("Documents related to the MAIN CONTRACTOR")]
        [EnglishName("Site Visits and Reports")]
        SiteVisitsAndReports,

        [ParentCategory("Admin")]
        [Abbreviation("12RFI")]
        [Category("Documents related to the MAIN CONTRACTOR")]
        [EnglishName("RFIs and related Documents")]
        RFIs,

        [ParentCategory("Admin")]
        [Abbreviation("12CN")]
        [Category("Documents related to the MAIN CONTRACTOR")]
        [EnglishName("All other documents related to a Main Contractor")]
        OtherMainContractorDocuments,

        // 13 - Documents related to SPECIALIST CONTRACTORS
        [ParentCategory("Admin")]
        [Abbreviation("13CD")]
        [Category("Documents related to SPECIALIST CONTRACTORS")]
        [EnglishName("Documents related to Contractor Design")]
        ContractorDesign,

        [ParentCategory("Admin")]
        [Abbreviation("13DM")]
        [Category("Documents related to SPECIALIST CONTRACTORS")]
        [EnglishName("Documents related to the Demolition and Strip-Out Contractor")]
        DemolitionContractor,

        [ParentCategory("Admin")]
        [Abbreviation("13EW")]
        [Category("Documents related to SPECIALIST CONTRACTORS")]
        [EnglishName("Documents related to the Enabling Works Contractor")]
        EnablingWorksContractor,

        [ParentCategory("Admin")]
        [Abbreviation("13FO")]
        [Category("Documents related to SPECIALIST CONTRACTORS")]
        [EnglishName("Documents related to the Fit-Out Contractor")]
        FitOutContractor,

        [ParentCategory("Admin")]
        [Abbreviation("13TC")]
        [Category("Documents related to SPECIALIST CONTRACTORS")]
        [EnglishName("Documents related to Trade Contractors")]
        TradeContractors,


        [EnglishName("Custom")]
        Custom,
    }

    public enum Role
    {
        [Abbreviation("AR")]
        [EnglishName("Architecture")]
        Architecture,

        [Abbreviation("LA")]
        [EnglishName("Landscape")]
        Landscape,

        [Abbreviation("IM")]
        [EnglishName("BIM Information Management")]
        BIMInformationManagement,

        [Abbreviation("PD")]
        [EnglishName("Principal Designer")]
        PrincipalDesigner,

        [EnglishName("Custom")]
        Custom,
    }

    public enum FileType
    {

        [Abbreviation("")]
        [Category("None")]
        [EnglishName("None of the above")]
        None,

        [Abbreviation("L")]
        [Category("Letter")]
        [EnglishName("Letter")]
        Letter,

        [Abbreviation("A")]
        [Category("Agenda")]
        [EnglishName("Agenda")]
        Agenda,

        [Abbreviation("M")]
        [Category("Minutes")]
        [EnglishName("Minutes")]
        Minutes,

        [Abbreviation("PR")]
        [Category("Publicity")]
        [EnglishName("Publicity")]
        Publicity,

        [EnglishName("Custom")]
        Custom,
    }

    public static class NamingLists
    {
        public static string? GetAbbreviation<T>(T value)
        {
            var field = value?.GetType().GetField(value.ToString());
            var attribute = (AbbreviationAttribute)field?.GetCustomAttributes(typeof(AbbreviationAttribute), false)?.FirstOrDefault();
            return attribute?.Abbreviation;
        }

        public static string? GetCategory<T>(T value)
        {
            var field = value?.GetType().GetField(value.ToString());
            var attribute = (CategoryAttribute)field?.GetCustomAttributes(typeof(CategoryAttribute), false)?.FirstOrDefault();
            return attribute?.Category;
        }

        public static string? GetParentCategory<T>(T value)
        {
            var field = value?.GetType().GetField(value.ToString());
            var attribute = (ParentCategoryAttribute)field?.GetCustomAttributes(typeof(ParentCategoryAttribute), false)?.FirstOrDefault();
            return attribute?.ParentCategory;
        }

        public static string? GetEnglishName<T>(T value)
        {
            var field = value?.GetType().GetField(value.ToString());
            var attribute = (EnglishNameAttribute)field?.GetCustomAttributes(typeof(EnglishNameAttribute), false)?.FirstOrDefault();
            return attribute?.EnglishName;
        }

        public static Type GetType(string name)
        {
            return Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(x => x.Name == name);
        }

        public static List<TreeViewItem>? GetTreeViewItems(Type t)
        {
            // group enum by parentCategory and category
            var values = Enum.GetValues(t);
            var treeViewItems = new List<TreeViewItem>();

            if (t == typeof(DocumentTypeIso))
                values = values.Cast<DocumentTypeIso>().OrderBy(x => GetAbbreviation(x)).ToArray();

            foreach (var value in values)
            {
                var parentCategory = GetParentCategory(value);  // Assuming this method gets the parent category
                var category = GetCategory(value);
                var abbreviation = GetAbbreviation(value);
                var englishName = GetEnglishName(value);

                // if custom, skip
                if (englishName == "Custom") continue;

                TreeViewItem? parentCategoryTreeViewItem = null;
                TreeViewItem? categoryTreeViewItem = null;
                TreeViewItem? treeViewItem = null;

                if (!String.IsNullOrEmpty(abbreviation))
                {
                    // if parentcategory is not null
                    // find parentCategoryTreeViewItem in treeViewItems
                    // if not found, create parentCategoryTreeViewItem
                    // find categoryTreeViewItem in parentCategoryTreeViewItem.Items
                    // if not found, create categoryTreeViewItem
                    // add categoryTreeViewItem to parentCategoryTreeViewItem.Items
                    // create treeViewItem
                    // add treeViewItem to categoryTreeViewItem.Items
                    // add parentCategoryTreeViewItem to treeViewItems

                    // else
                    // find categoryTreeViewItem in treeViewItems
                    // if not found, create categoryTreeViewItem
                    // create treeViewItem
                    // add treeViewItem to categoryTreeViewItem.Items
                    // add categoryTreeViewItem to treeViewItems

                    treeViewItem = new TreeViewItem { Header = $"{abbreviation} - {englishName}", Tag = value };
                    
                    if (parentCategory != null)
                    {
                        parentCategoryTreeViewItem = treeViewItems.FirstOrDefault(x => x.Header?.ToString()?.ToLower() == parentCategory.ToLower());

                        if (parentCategoryTreeViewItem == null)
                        {
                            parentCategoryTreeViewItem = new TreeViewItem { Header = parentCategory };
                            treeViewItems.Add(parentCategoryTreeViewItem);
                        }


                        if (category != null)
                        {
                            var categoryHeader = abbreviation.Length > 2 ? $"{abbreviation?.Substring(0, 2)}X - {category}" : $"{abbreviation?.First()}X - {category}";
                            categoryTreeViewItem = parentCategoryTreeViewItem.Items.Cast<TreeViewItem>().FirstOrDefault(x => x.Header?.ToString()?.ToLower() == categoryHeader.ToLower());

                            if (categoryTreeViewItem == null)
                            {
                                categoryTreeViewItem = new TreeViewItem { Header = categoryHeader };
                                parentCategoryTreeViewItem.Items.Add(categoryTreeViewItem);
                            }

                            categoryTreeViewItem.Items.Add(treeViewItem);
                        }
                        else
                            parentCategoryTreeViewItem.Items.Add(treeViewItem);
                    }

                    else if(category != null)
                    {
                        var categoryHeader = abbreviation.Length > 2 ? $"{abbreviation?.Substring(0, 2)}X - {category}" : $"{abbreviation?.First()}X - {category}";
                        categoryTreeViewItem = treeViewItems.FirstOrDefault(x => x.Header?.ToString()?.ToLower() == categoryHeader.ToLower());

                        if (categoryTreeViewItem == null)
                        {
                            categoryTreeViewItem = new TreeViewItem { Header = categoryHeader };
                            treeViewItems.Add(categoryTreeViewItem);
                        }

                        categoryTreeViewItem.Items.Add(treeViewItem);
                    }
                    else
                        treeViewItems.Add(treeViewItem);
                }
            }

            return treeViewItems;
        }


        public static List<TreeViewItem>? GetTreeViewItems_Ss(Type t)
        {
            // group enum by category
            // // if there is more than one category, create parent treeviewitem with category name
            // and add children with enum values
            var values = Enum.GetValues(t);
            var treeViewItems = new List<TreeViewItem>();

            if(t == typeof(DocumentTypeIso))
                values = values.Cast<DocumentTypeIso>().OrderBy(x => GetAbbreviation(x)).ToArray();

            foreach (var value in values)
            {
                var category = GetCategory(value);
                var abbreviation = GetAbbreviation(value);
                //if(abbreviation == null) continue; // skip if abbreviation is null (not needed in treeview
                //if(String.IsNullOrEmpty(abbreviation)) continue; // skip if abbreviation is empty (not needed in treeview
                var englishName = GetEnglishName(value);

                // if custom, skip
                if (englishName == "Custom") continue;


                if (category != null && !String.IsNullOrEmpty(abbreviation))
                {
                    var header = abbreviation.Length > 2 ? $"{abbreviation?.Substring(0,2)}X - {category}" : $"{abbreviation?.First()}X - {category}";
                    var categoryTreeViewItem = treeViewItems.FirstOrDefault(x => x.Header?.ToString()?.ToLower() == header.ToLower());
                    
                    if (categoryTreeViewItem == null)
                    {
                        categoryTreeViewItem = new TreeViewItem { Header = header, Tag = value };
                        treeViewItems.Add(categoryTreeViewItem);
                    }

                    var treeViewItem = new TreeViewItem { Header = $"{abbreviation} - {englishName}", Tag = value };
                    categoryTreeViewItem.Items.Add(treeViewItem);
                }
                else
                {
                    var treeViewItem = new TreeViewItem { Header = $"{abbreviation} - {englishName}", Tag = value };
                    treeViewItems.Add(treeViewItem);
                }
            }

            return treeViewItems;
        }

        internal static object GetValue(Type t, string v)
        {
            return Enum.Parse(t, v);
        }
    }
}
