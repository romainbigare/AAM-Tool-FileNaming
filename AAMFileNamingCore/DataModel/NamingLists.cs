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
        [Abbreviation("")]
        [EnglishName("None")]
        None,
        [Abbreviation("DR")]
        [EnglishName("Drawing")]
        Drawing,
        [Abbreviation("SK")]
        [EnglishName("Sketch")]
        Sketch,
        [Abbreviation("SH")]
        [EnglishName("Schedule")]
        Schedule,
        [Abbreviation("SP")]
        [EnglishName("Specification")]
        Specification,
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
        [Abbreviation("M3")]
        [EnglishName("3D Model")]
        Model3D,
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


        [Abbreviation("SK")]
        [EnglishName("Sketch (can be a CAD file, hand sketch, diagram, or other)")]
        Sketch,

        [Abbreviation("PR")]
        [EnglishName("Press Release")]
        PressRelease,

        [Abbreviation("TR")]
        [EnglishName("Tracking Log")]
        TrackingLog,

        [Abbreviation("DIR")]
        [EnglishName("Document Issue Register")]
        DocumentIssueRegister,

        [Abbreviation("DIS")]
        [EnglishName("Document Issue Sheet")]
        DocumentIssueSheet,

        [Abbreviation("R")]
        [EnglishName("Report to be printed or read on screen")]
        Report,

        [Abbreviation("P")]
        [EnglishName("Presentation, Slides for a meeting or talk. Also includes Videos.")]
        Presentation,

        [Abbreviation("00X")]
        [Category("Documents for Internal Circulation")]
        [EnglishName("Documents created for Internal Circulation")]
        DocumentForInternalCirculation,

        // Documents related to the CLIENT
        [Abbreviation("01A")]
        [Category("Documents related to the CLIENT")]
        [EnglishName("Documents related to the Clients Agent")]
        ClientAgent,

        [Abbreviation("01B")]
        [Category("Documents related to the CLIENT")]
        [EnglishName("Client brief and related documents")]
        ClientBrief,

        [Abbreviation("01C")]
        [Category("Documents related to the CLIENT")]
        [EnglishName("Client Correspondence (except fee letters)")]
        ClientCorrespondence,

        [Abbreviation("01CR")]
        [Category("Documents related to the CLIENT")]
        [EnglishName("Client Change Request")]
        ClientChangeRequest,

        [Abbreviation("01D")]
        [Category("Documents related to the CLIENT")]
        [EnglishName("Other Client documents")]
        OtherClientDocuments,

        [Abbreviation("01F")]
        [Category("Documents related to the CLIENT")]
        [EnglishName("Fee letters, appointments and other related documents")]
        FeeLettersAndAppointments,

        // Documents related to the DESIGN
        [Abbreviation("02DEL")]
        [Category("Documents related to the DESIGN")]
        [EnglishName("Project Deliverables")]
        ProjectDeliverables,

        [Abbreviation("02DN")]
        [Category("Documents related to the DESIGN")]
        [EnglishName("Design Notes")]
        DesignNotes,

        [Abbreviation("02PR")]
        [Category("Documents related to the DESIGN")]
        [EnglishName("Programmes")]
        Programmes,

        [Abbreviation("02R")]
        [Category("Documents related to the DESIGN")]
        [EnglishName("Design Risks / Risk Registers")]
        DesignRisks,

        [Abbreviation("02RE")]
        [Category("Documents related to the DESIGN")]
        [EnglishName("Research")]
        Research,

        [Abbreviation("02REP")]
        [Category("Documents related to the DESIGN")]
        [EnglishName("Reports")]
        Reports,

        [Abbreviation("02QA")]
        [Category("Documents related to the DESIGN")]
        [EnglishName("All Schedules (Areas, Doors, etc).")]
        Schedules,

        [Abbreviation("02RF")]
        [Category("Documents related to the DESIGN")]
        [EnglishName("Reference or Precedent Documents and Images")]
        ReferenceDocuments,

        [Abbreviation("02SI")]
        [Category("Documents related to the DESIGN")]
        [EnglishName("Documents and Images related to the Site / Site Visits.")]
        SiteDocuments,

        // Documents related to COMPLIANCE
        [Abbreviation("03BR")]
        [Category("Documents related to COMPLIANCE")]
        [EnglishName("Documents related to Building Regulations")]
        BuildingRegulations,

        [Abbreviation("03EA")]
        [Category("Documents related to COMPLIANCE")]
        [EnglishName("Documents related to Environmental Agency Compliance")]
        EnvironmentalAgencyCompliance,

        [Abbreviation("03EI")]
        [Category("Documents related to COMPLIANCE")]
        [EnglishName("Documents related to Environmental Impact Compliance")]
        EnvironmentalImpactCompliance,

        [Abbreviation("03LDA")]
        [Category("Documents related to COMPLIANCE")]
        [EnglishName("Documents related to London Development Agency Compliance")]
        LondonDevelopmentAgencyCompliance,

        [Abbreviation("03HS")]
        [Category("Documents related to COMPLIANCE")]
        [EnglishName("CDM - Health and Safety")]
        HealthAndSafety,

        [Abbreviation("03PC")]
        [Category("Documents related to COMPLIANCE")]
        [EnglishName("Documents related to Public Consultations")]
        PublicConsultations,

        [Abbreviation("03PD")]
        [Category("Documents related to COMPLIANCE")]
        [EnglishName("CDM - Principal Designer")]
        PrincipalDesigner,

        [Abbreviation("03PL")]
        [Category("Documents related to COMPLIANCE")]
        [EnglishName("Documents related to Planning Compliance")]
        PlanningCompliance,

        [Abbreviation("03PW")]
        [Category("Documents related to COMPLIANCE")]
        [EnglishName("Documents related to Party Wall Compliance")]
        PartyWallCompliance,

        [Abbreviation("03RL")]
        [Category("Documents related to COMPLIANCE")]
        [EnglishName("Documents related to Rights To Light Compliance")]
        RightsToLightCompliance,

        [Abbreviation("03RP")]
        [Category("Documents related to COMPLIANCE")]
        [EnglishName("Documents related to Royal Parks Authority Compliance")]
        RoyalParksAuthorityCompliance,

        [Abbreviation("03TP")]
        [Category("Documents related to COMPLIANCE")]
        [EnglishName("Documents related to Tree Protection Compliance")]
        TreeProtectionCompliance,

        // Document related to the QUANTITY SURVEYOR / COSTS
        [Abbreviation("04C")]
        [Category("Document related to the QUANTITY SURVEYOR / COSTS")]
        [EnglishName("Letters to Cost Consultant / Quantity Surveyor")]
        LettersToCostConsultant,

        [Abbreviation("04QS")]
        [Category("Document related to the QUANTITY SURVEYOR / COSTS")]
        [EnglishName("Documents related to Costs / Quantities")]
        CostsAndQuantities,

        [Abbreviation("04M")]
        [Category("Document related to the QUANTITY SURVEYOR / COSTS")]
        [EnglishName("Meetings related to Costs / Quantities.")]
        CostMeetings,

        // Documents related to the STRUCTURAL DESIGN or ENGINEER
        [Abbreviation("05C")]
        [Category("Documents related to the STRUCTURAL DESIGN or ENGINEER")]
        [EnglishName("Letters to the Structural Engineer")]
        LettersToStructuralEngineer,

        [Abbreviation("05SE")]
        [Category("Documents related to the STRUCTURAL DESIGN or ENGINEER")]
        [EnglishName("Documents related to Structural Engineering")]
        StructuralEngineering,

        // Document related to the SERVICES DESIGN or ENGINEER
        [Abbreviation("06C")]
        [Category("Document related to the SERVICES DESIGN or ENGINEER")]
        [EnglishName("Letters to the Services Engineer")]
        LettersToServicesEngineer,

        [Abbreviation("06MEP")]
        [Category("Document related to the SERVICES DESIGN or ENGINEER")]
        [EnglishName("Documents related to Services Engineering")]
        ServicesEngineering,

        // Documents related to SPECIALISTS and SPECIALIST AREAS
        [Abbreviation("07106")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Section 106 Agreement / Consultant")]
        Section106,

        [Abbreviation("07AA")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Adjacent Site Architects")]
        AdjacentSiteArchitects,

        [Abbreviation("07AC")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Acoustic Design / Consultant")]
        AcousticDesign,

        [Abbreviation("07ACC")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Access Design / Consultant")]
        AccessDesign,

        [Abbreviation("07AO")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Adjoining Owners Agent")]
        AdjoiningOwnersAgent,

        [Abbreviation("07AR")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Art Consultant")]
        ArtConsultant,

        [Abbreviation("07ARB")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Tree Specialist")]
        TreeSpecialist,

        [Abbreviation("07ARC")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to Archaeology / the Archaeologist")]
        Archaeology,

        [Abbreviation("07AS")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Asbestos Strategy / Surveyor")]
        AsbestosStrategy,

        [Abbreviation("07AV")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Audio Visual Design / Consultant")]
        AudioVisualDesign,

        [Abbreviation("07BIM")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to BIM or BIM Managers / Consultants")]
        BIM,

        [Abbreviation("07BR")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to BREEAM / BREEAM Consultants")]
        BREEAM,

        [Abbreviation("07C20")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Twentieth Century Society")]
        TwentiethCenturySociety,

        [Abbreviation("07CA")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Catering Strategy / Specialist")]
        CateringStrategy,

        [Abbreviation("07CE")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Civil Design / Engineer")]
        CivilDesign,

        [Abbreviation("07CL")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Cladding Consultant")]
        CladdingConsultant,

        [Abbreviation("07CLP")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Clinic Planner")]
        ClinicPlanner,

        [Abbreviation("07CM")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Computer Modeller")]
        ComputerModeller,

        [Abbreviation("07CMM")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Comms. Design / Consultant")]
        CommsDesign,

        [Abbreviation("07CO")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Conservation Strategy / Specialist")]
        ConservationStrategy,

        [Abbreviation("07CP")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Construction Programmer")]
        ConstructionProgrammer,

        [Abbreviation("07CS")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to Building Conditions / the Conditions Surveyor")]
        BuildingConditions,

        [Abbreviation("07CT")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to Coatings / the Coatings Specialist")]
        Coatings,

        [Abbreviation("07CW")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Clerk Of Works")]
        ClerkOfWorks,

        [Abbreviation("07DR")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Drainage Strategy / Specialist")]
        DrainageStrategy,

        [Abbreviation("07DS")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to Daylight Sunlight / Specialist")]
        DaylightSunlight,

        [Abbreviation("07EC")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Environmental Strategy / Consultant")]
        EnvironmentalStrategy,

        [Abbreviation("07EL")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Electrical Services Design / Consultant")]
        ElectricalServicesDesign,

        [Abbreviation("07EOD")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Explosive Ordnance Disposal")]
        ExplosiveOrdnanceDisposal,

        [Abbreviation("07EX")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Exhibition Design / Designer")]
        ExhibitionDesign,

        [Abbreviation("07FA")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Facade Design / Specialist")]
        FacadeDesign,

        [Abbreviation("07FL")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Flood Design / Consultant")]
        FloodDesign,

        [Abbreviation("07FO")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Fit-Out Design / Specialist")]
        FitOutDesign,

        [Abbreviation("07FR")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Fire Strategy / Consultant")]
        FireStrategy,

        [Abbreviation("07FRA")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Flood Risk Strategy / Assessor")]
        FloodRiskStrategy,

        [Abbreviation("07FU")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to Funding / the Funding Agent")]
        Funding,

        [Abbreviation("07GD")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Graphic Designer")]
        GraphicDesigner,

        [Abbreviation("07GS")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Geotechnical Design / Specialist")]
        GeotechnicalDesign,

        [Abbreviation("07HI")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Historian")]
        Historian,

        [Abbreviation("07HO")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Hospitality Strategy / Consultants")]
        HospitalityStrategy,

        [Abbreviation("07HP")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Healthcare Strategy / Planner")]
        HealthcareStrategy,

        [Abbreviation("07HT")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Hotel Design / Specialists")]
        HotelDesign,

        [Abbreviation("07HW")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Highways Strategy / Consultant")]
        HighwaysStrategy,

        [Abbreviation("07HZ")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Hazardous Materials / Surveyor")]
        HazardousMaterials,

        [Abbreviation("07IB")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to Insurance / the Insurance Broker")]
        Insurance,

        [Abbreviation("07ID")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Interior Design / Designer")]
        InteriorDesign,

        [Abbreviation("07INT")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Interpreter - Translator")]
        Interpreter,

        [Abbreviation("07IT")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the IT Strategy / Specialist")]
        ITStrategy,

        [Abbreviation("07KT")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Kitchen Design / Specialist")]
        KitchenDesign,

        [Abbreviation("07LA")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Landscape Design / Architect")]
        LandscapeDesign,

        [Abbreviation("07LF")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Lift Strategy / Consultant")]
        LiftStrategy,

        [Abbreviation("07LG")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Local Interest Groups")]
        LocalInterestGroups,

        [Abbreviation("07LI")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Lighting Design / Consultant")]
        LightingDesign,

        [Abbreviation("07LM")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Landscape Manager")]
        LandscapeManager,

        [Abbreviation("07LO")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Landowner")]
        Landowner,

        [Abbreviation("07LR")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Local Residents")]
        LocalResidents,

        [Abbreviation("07LW")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Legal Agent")]
        LegalAgent,

        [Abbreviation("07MA")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Maintenance + Acccess Strategy / Specialist")]
        MaintenanceAccessStrategy,

        [Abbreviation("07MA")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Masterplanners")]
        Masterplanners,

        [Abbreviation("07MAS")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Masonry Design / Specialist")]
        MasonryDesign,

        [Abbreviation("07MET")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Metallurgist")]
        Metallurgist,

        [Abbreviation("07MM")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to Modelmaking / the Modelmakers")]
        Modelmaking,

        [Abbreviation("07MP")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Model Photographer")]
        ModelPhotographer,

        [Abbreviation("07MT")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Model Transporter")]
        ModelTransporter,

        [Abbreviation("07OA")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Office Agent")]
        OfficeAgent,

        [Abbreviation("07OM")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the O&M Manual Specialist")]
        OMManualSpecialist,

        [Abbreviation("07PA")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Perspective Artist")]
        PerspectiveArtist,

        [Abbreviation("07PF")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to People Flow (Crowd Planning) / Consultant")]
        PeopleFlow,

        [Abbreviation("07PH")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to Photography / the Photographer")]
        Photography,

        [Abbreviation("07PHC")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Public Health Strategy / Consultant")]
        PublicHealthStrategy,

        [Abbreviation("07PL")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to Planning / the Planning Consultant")]
        Planning,

        [Abbreviation("07PR")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to Public Relations")]
        PublicRelations,

        [Abbreviation("07PRC")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Property Consultant")]
        PropertyConsultant,

        [Abbreviation("07PRM")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Property Manager")]
        PropertyManager,

        [Abbreviation("07PV")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to Photovoltaics")]
        Photovoltaics,

        [Abbreviation("07PW")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Party Wall Strategy / Surveyor")]
        PartyWallStrategy,

        [Abbreviation("07RA")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Retail Agent")]
        RetailAgent,

        [Abbreviation("07RC")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Retail Consultant")]
        RetailConsultant,

        [Abbreviation("07RESA")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Residential Agent")]
        ResidentialAgent,

        [Abbreviation("07RI")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Risk Consultant")]
        RiskConsultant,

        [Abbreviation("07RL")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Rights To Light Surveyor")]
        RightsToLightSurveyor,

        [Abbreviation("07SE")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Security Strategy / Consultant")]
        SecurityStrategy,

        [Abbreviation("07SG")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Solar Glare Specialist")]
        SolarGlareSpecialist,

        [Abbreviation("07SP")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to Space Planning / the Space Planners")]
        SpacePlanning,

        [Abbreviation("07ST")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Studio Specialist")]
        StudioSpecialist,

        [Abbreviation("07SU")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to Surveys / the Surveyor")]
        Surveys,

        [Abbreviation("07SUS")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Sustainability Strategy / Consultant")]
        SustainabilityStrategy,

        [Abbreviation("07TH")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Theatre Design / Consultant")]
        TheatreDesign,

        [Abbreviation("07TN")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Townscape Strategy / Consultant")]
        TownscapeStrategy,

        [Abbreviation("07TR")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Traffic or Transport Strategy / Consultant")]
        TransportStrategy,

        [Abbreviation("07UD")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Urban Designer")]
        UrbanDesigner,

        [Abbreviation("07VIS")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to Visualisation / the Visualisation Consultant")]
        Visualisation,

        [Abbreviation("07WC")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Wind Strategy / Consultant")]
        WindStrategy,

        [Abbreviation("07WF")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Wayfinding Design / Consultant")]
        WayfindingDesign,

        [Abbreviation("07WR")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Waste And Recycling Strategy / Specialist")]
        WasteAndRecyclingStrategy,

        [Abbreviation("07XA")]
        [Category("Documents related to SPECIALISTS and SPECIALIST AREAS")]
        [EnglishName("Documents related to the Executive Architect")]
        ExecutiveArchitect,

        // Documents related to the Design / Project TEAM
        [Abbreviation("08C")]
        [Category("Documents related to the Design / Project TEAM")]
        [EnglishName("Letters to the Design / Project Team")]
        LettersToDesignTeam,

        [Abbreviation("08CA")]
        [Category("Documents related to the Design / Project TEAM")]
        [EnglishName("Consultants Appointments and related Documents")]
        ConsultantAppointments,

        [Abbreviation("08CC")]
        [Category("Documents related to the Design / Project TEAM")]
        [EnglishName("Documents related to Change Control")]
        ChangeControl,

        [Abbreviation("08DT")]
        [Category("Documents related to the Design / Project TEAM")]
        [EnglishName("Documents related to the Design Team")]
        DesignTeam,

        [Abbreviation("08MD")]
        [Category("Documents related to the Design / Project TEAM")]
        [EnglishName("Design Team Meetings (e.g Agendas, Minutes)")]
        DesignTeamMeetings,

        [Abbreviation("08PD")]
        [Category("Documents related to the Design / Project TEAM")]
        [EnglishName("Project Directory and related Documents")]
        ProjectDirectory,

        [Abbreviation("08PM")]
        [Category("Documents related to the Design / Project TEAM")]
        [EnglishName("Documents related to the Project Management")]
        ProjectManagement,

        [Abbreviation("08PR")]
        [Category("Documents related to the Design / Project TEAM")]
        [EnglishName("Team Programme and related Documents")]
        TeamProgramme,

        [Abbreviation("08MP")]
        [Category("Documents related to the Design / Project TEAM")]
        [EnglishName("Project Team Meetings (e.g Agendas, Minutes)")]
        ProjectTeamMeetings,

        [Abbreviation("08PT")]
        [Category("Documents related to the Design / Project TEAM")]
        [EnglishName("Documents related to the Project Team")]
        ProjectTeam,

        [Abbreviation("08RM")]
        [Category("Documents related to the Design / Project TEAM")]
        [EnglishName("Responsibility Matrix and related Documents")]
        ResponsibilityMatrix,

        [Abbreviation("08SC")]
        [Category("Documents related to the Design / Project TEAM")]
        [EnglishName("Documents related to Subconsultants")]
        Subconsultants,

        [Abbreviation("08VE")]
        [Category("Documents related to the Design / Project TEAM")]
        [EnglishName("Documents related to Value Engineering")]
        ValueEngineering,

        // Documents related to the TECHNICAL Design
        [Abbreviation("09AMT")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the AMT package")]
        AMTPackage,

        [Abbreviation("09CLG")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the CLG package")]
        CLGPackage,

        [Abbreviation("09EES")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the EES package")]
        EESPackage,

        [Abbreviation("09EXW")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the EXW package")]
        EXWPackage,

        [Abbreviation("09FFE")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the FFE package")]
        FFEPackage,

        [Abbreviation("09FIN")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the FIN package")]
        FINPackage,

        [Abbreviation("09FLS")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the FLS package")]
        FLSPackage,

        [Abbreviation("09FPS")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the FPS package")]
        FPSPackage,

        [Abbreviation("09IDR")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the IDR package")]
        IDRPackage,

        [Abbreviation("09ILP")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the ILP package")]
        ILPPackage,

        [Abbreviation("09IPS")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the IPS package")]
        IPSPackage,

        [Abbreviation("09ITW")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the ITW package")]
        ITWPackage,

        [Abbreviation("09JNY")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the JNY package")]
        JNYPackage,

        [Abbreviation("09KIT")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the KIT package")]
        KITPackage,

        [Abbreviation("09MAE")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the MAE package")]
        MAEPackage,

        [Abbreviation("09RFS")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the RFS package")]
        RFSPackage,

        [Abbreviation("09SAN")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the SAN package")]
        SANPackage,

        [Abbreviation("09SCR")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the SCR package")]
        SCRPackage,

        [Abbreviation("09SGN")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the SGN package")]
        SGNPackage,

        [Abbreviation("09TD")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents created by the project Technical Director")]
        TechnicalDirector,

        [Abbreviation("09USD")]
        [Category("Documents related to the TECHNICAL Design")]
        [EnglishName("Documents related to the USD package")]
        USDPackage,

        // 12 - Documents related to the MAIN CONTRACTOR
        [Abbreviation("12C")]
        [Category("Documents related to the MAIN CONTRACTOR")]
        [EnglishName("Letters to a Main Contractor")]
        LettersToMainContractor,

        [Abbreviation("12SR")]
        [Category("Documents related to the MAIN CONTRACTOR")]
        [EnglishName("Site Visits and Reports")]
        SiteVisitsAndReports,

        [Abbreviation("12RFI")]
        [Category("Documents related to the MAIN CONTRACTOR")]
        [EnglishName("RFIs and related Documents")]
        RFIs,

        [Abbreviation("12CN")]
        [Category("Documents related to the MAIN CONTRACTOR")]
        [EnglishName("All other documents related to a Main Contractor")]
        OtherMainContractorDocuments,

        // 13 - Documents related to SPECIALIST CONTRACTORS
        [Abbreviation("13CD")]
        [Category("Documents related to SPECIALIST CONTRACTORS")]
        [EnglishName("Documents related to Contractor Design")]
        ContractorDesign,

        [Abbreviation("13DM")]
        [Category("Documents related to SPECIALIST CONTRACTORS")]
        [EnglishName("Documents related to the Demolition and Strip-Out Contractor")]
        DemolitionContractor,

        [Abbreviation("13EW")]
        [Category("Documents related to SPECIALIST CONTRACTORS")]
        [EnglishName("Documents related to the Enabling Works Contractor")]
        EnablingWorksContractor,

        [Abbreviation("13FO")]
        [Category("Documents related to SPECIALIST CONTRACTORS")]
        [EnglishName("Documents related to the Fit-Out Contractor")]
        FitOutContractor,

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
