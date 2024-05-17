using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AAMFileNaming
{
    internal static class Guesser
    {
        public static async Task<bool> GuessFromExistingNonIsoName(File file)
        {
            // proj number _ file code _  document type _  date of issue _ rev _ descriptiion
            var names = file.Name.Split('_');

            if (names.Length > 0)
                file.ProjectNumber = names[0];
            else
                file.ProjectNumber = await GuessProjectNumber(file.Path);
            file.Originator = "AAM";

            if (names.Length > 1)
                file.FileCode = (FileCode)NamingLists.GetValue(typeof(FileCode), names[1]);
            else
                file.FileCode = await GuessFileCode(file.Path);

            if (names.Length > 2)
                file.DocumentType = (DocumentType)NamingLists.GetValue(typeof(DocumentType), names[2]);
            else
                file.DocumentType = await GuessDocumentType(file.Path, file.Extension, file.Name);

            if (names.Length > 3)
                file.DateOfIssue = names[3];
            else
                file.DateOfIssue = await GuessDateOfIssue(file.Path);

            if (names.Length > 5)
                file.Revision = names[5];
            else
                file.Revision = "";

            if (names.Length > 5)
                file.Description = names[6];
            else
                file.Description = "";


            file.Role = await GuessRole(file.Path);
            file.TypeCode = await GuessTypeCode(file.Path, file.Extension);
            file.DrawingSerialNo = await GuessDrawingSerialNo();
            file.FileType = await GuessFileType();
            (file.Description, file.Volume, file.Level) = await GuessDocumentContent(file.Name, file.Extension);
            file.Revision = "";

            return true;
        }


        public static async Task<bool> GuessFromExistingIsoName(File file)
        {
            // ProjectNumber =  // whatever is before the first underscore in name
            var names = file.Name.Split('-');

            if (names.Length > 0)
                file.ProjectNumber = names[0];
            else
                file.ProjectNumber = await GuessProjectNumber(file.Path);

            file.Originator = "AAM";

            if (names.Length > 1)
                file.Volume = names[1];
            else
                file.Volume = GuessVolume(file.Name);
            if (names.Length > 2)
                file.Level = (Level)NamingLists.GetValue(typeof(Level), names[2]);
            else
                file.Level = GuessLevel(file.Name, file.Extension);

            if (names.Length > 3)
                file.DocumentType = (DocumentType)NamingLists.GetValue(typeof(DocumentType), names[3]);
            else
                file.DocumentType = await GuessDocumentType(file.Path, file.Extension, file.Name);

            if (names.Length > 4)
                file.Role = (Role)NamingLists.GetValue(typeof(Role), names[4]);
            else
                file.Role = await GuessRole(file.Path);

            if (names.Length > 5 && names[5].Length > 5)
                file.FileCode = (FileCode)NamingLists.GetValue(typeof(FileCode), names[5].Substring(0, 2));
            else
                file.FileCode = await GuessFileCode(file.Path);

            if (names.Length > 5 && names[5].Length > 5)
                file.DrawingSerialNo = names[6].Substring(2, 4);
            else
                file.DrawingSerialNo = await GuessDrawingSerialNo();

            if (names.Length > 5 && names[5].Length > 5)
            {
                var last = names[5].Split('_');
                if (last.Length > 1)
                    file.Description = last[1];
                else
                    file.Description = "";
            }

            file.TypeCode = await GuessTypeCode(file.Path, file.Extension);
            file.DateOfIssue = await GuessDateOfIssue(file.Path);
            file.FileType = await GuessFileType();
            file.Revision = "";

            return true;
        }


        public static async Task<FileType> GuessFileType()
        {
            // TO DO : Implement this method
            return FileType.None;
        }

        public static async Task<(string Description, string Volume, Level Level)> GuessDocumentContent(string Name, string Extension )
        {
            // TO DO: Implement this method
            return ("", GuessVolume(Name), GuessLevel(Name, Extension));
        }

        public static Level GuessLevel(string Name, string Extension)
        {
            string[] fileformats3D = { "3dm", "3ds", "dwg", "dxf", "fbx", "ifc", "iges", "obj", "rvt", "rfa", "skp", "stl", "step", "stl", "wrl", "x_t" };
            var extension = Extension.ToLower().Replace(".", "");

            if (fileformats3D.Contains(extension))
                return Level.MultipleLevels;

            // if pdf or dgn and name contains a number, return that number
            if (extension == "pdf" || extension == "dgn")
            {
                var name = Name.ToLower();
                var number = Regex.Match(name, @"\d+").Value;
                if (number != "")
                    return GetLevelFromNumber(number);
            }

            return Level.NoLevelApplicable;

        }

        public static Level GetLevelFromNumber(string number)
        {
            // if number is 1-9, add 0, then find Level from number
            Level level = Level.NoLevelApplicable;
            try
            {
                if (number.Length == 1)
                    level = (Level)Enum.Parse(typeof(Level), "Level" + "0" + number);
                else
                    level = (Level)Enum.Parse(typeof(Level), "Level" + number);
            }
            catch
            {
                level = Level.NoLevelApplicable;
            }

            return level;
        }

        public static string GuessVolume(string Name)
        {
            var name = Name.ToLower();

            // examples : 
            /*/
                  something_plot_A_something.pdf
            something_plotA_something.pdf
            something_PlotA.pdf
            something_plot_A.pdf
            plotA_something.pdf
            something-plot-A.pdf
            something-plotA-something.pdf
            something-building1.pdf
            something-building-1.pdf
            something-building-01.pdf
            something-Building01.pdf
            something-building-01A.pdf
            something-building-01-A.pdf
            something-building-01-A-something.pdf
            /*/

            // extract A or 01, or 1 from the names above, if the name contains plot, building, block
            var discardedValues = new string[] { "s", "aam", "regs", "width", "with" };
            var volume = Regex.Match(name, @"plot|building|block");
            if (volume.Success)
            {
                // extract the volume number or name after the plot building or block. it can be plotA, plot-A, plot_A, plot 1, plot 01, plot 01A, plot 01-A, plot 01-A-something, plot ABCD, plot ABCD1, plot A1B1, etc
                var volumeNumber = Regex.Match(name, @"(?<=plot|building|block)[\s_-]*[a-zA-Z0-9]+").Value;

                volumeNumber = volumeNumber.Trim().Replace(" ", "").Replace("_", "").Replace("-", "");
                // if the volume number is not empty, return it
                if (volumeNumber.Length < 5 && volumeNumber.Length > 0 && !discardedValues.Contains(volumeNumber.ToLower()))
                    return volumeNumber.Length <2 && char.IsDigit(volumeNumber[0]) ? "0"+ volumeNumber.ToUpper() : volumeNumber.ToUpper();
            }

            return "XX";
        }

        public static async Task<FileCode> GuessFileCode(string Path)
        {
            var path = Path.ToLower();

            if (!path.Contains("admin"))
                return await FindNonAdminFileCode(path);

            if (path.Contains("01_client"))
            {
                if (path.Contains("01a_clientsagent"))
                    return FileCode.ClientAgent;

                if (path.Contains("01b_brief"))
                    return FileCode.ClientBrief;

                if (path.Contains("01c_correspondence"))
                    return FileCode.ClientCorrespondence;

                if (path.Contains("01cr_changerequest"))
                    return FileCode.ClientChangeRequest;

                if (path.Contains("01d_document"))
                    return FileCode.OtherClientDocuments;

                if (path.Contains("01f_feesappointments"))
                    return FileCode.FeeLettersAndAppointments;
            }

            if (path.Contains("02_designinfo"))
            {
                if (path.Contains("02del_deliverables"))
                    return FileCode.ProjectDeliverables;

                if (path.Contains("02dn_designnote"))
                    return FileCode.DesignNotes;

                if (path.Contains("02pr_programme"))
                    return FileCode.Programmes;

                if (path.Contains("02r_risk"))
                    return FileCode.DesignRisks;

                if (path.Contains("02re_research"))
                    return FileCode.Research;

                if (path.Contains("2rep_reports"))
                    return FileCode.Reports;

                if (path.Contains("02qa_schedules"))
                    return FileCode.Schedules;

                if (path.Contains("02rf_reference"))
                    return FileCode.ReferenceDocuments;

                if (path.Contains("02si_site"))
                    return FileCode.SiteDocuments;
            }

            if (path.Contains("03_compliance"))
            {
                if (path.Contains("03br_buildingregs"))
                    return FileCode.BuildingRegulations;

                if (path.Contains("03ea_envagency"))
                    return FileCode.EnvironmentalAgencyCompliance;

                if (path.Contains("03ei_envimpact"))
                    return FileCode.EnvironmentalImpactCompliance;

                if (path.Contains("03lda_londondev"))
                    return FileCode.LondonDevelopmentAgencyCompliance;

                if (path.Contains("03hs_cdmhealthandsafety"))
                    return FileCode.HealthAndSafety;

                if (path.Contains("03pc_publicconsult"))
                    return FileCode.PublicConsultations;

                if (path.Contains("03pd_cdmprincipaldesigner"))
                    return FileCode.PrincipalDesigner;

                if (path.Contains("03pl_planning"))
                    return FileCode.Planning;

                if (path.Contains("03pw_partywall"))
                    return FileCode.PartyWallStrategy;

                if (path.Contains("03rl_rightstolight"))
                    return FileCode.RightsToLightCompliance;

                if (path.Contains("03rp_royalparks"))
                    return FileCode.RoyalParksAuthorityCompliance;

                if (path.Contains("03tp_treeprotection"))
                    return FileCode.TreeProtectionCompliance;
            }

            if (path.Contains("04_costs"))
            {
                if (path.Contains("04c_letters"))
                    return FileCode.LettersToCostConsultant;

                if (path.Contains("04qs_documents"))
                    return FileCode.CostsAndQuantities;

                if (path.Contains("04m_meetings"))
                    return FileCode.CostMeetings;
            }

            if (path.Contains("05_structure"))
            {
                if (path.Contains("05se_letters"))
                    return FileCode.LettersToStructuralEngineer;

                if (path.Contains("05se_documents"))
                    return FileCode.StructuralEngineering;
            }

            if (path.Contains("06_services"))
            {
                if (path.Contains("06c_letters"))
                    return FileCode.LettersToServicesEngineer;

                if (path.Contains("06mep_documents"))
                    return FileCode.ServicesEngineering;
            }

            if (path.Contains("07_specialists"))
            {
                if (path.Contains("07dr_drainage"))
                    return FileCode.DrainageStrategy;

                if (path.Contains("07ds_daylightsunlight"))
                    return FileCode.DaylightSunlight;

                if (path.Contains("07ec_environment"))
                    return FileCode.EnvironmentalStrategy;

                if (path.Contains("07el_electrical"))
                    return FileCode.ElectricalServicesDesign;

                if (path.Contains("07eod_explosiveordnance"))
                    return FileCode.ExplosiveOrdnanceDisposal;

                if (path.Contains("07ex_exhibitiondesign"))
                    return FileCode.ExhibitionDesign;

                if (path.Contains("07fa_facade"))
                    return FileCode.FacadeDesign;

                if (path.Contains("07fl_flood"))
                    return FileCode.FloodDesign;

                if (path.Contains("07fo_fitout"))
                    return FileCode.FitOutDesign;

                if (path.Contains("07fr_fire"))
                    return FileCode.FireStrategy;

                if (path.Contains("07fra_floodrisk"))
                    return FileCode.FloodRiskStrategy;

                if (path.Contains("07fu_funding"))
                    return FileCode.Funding;

                if (path.Contains("07gd_graphics"))
                    return FileCode.GraphicDesigner;

                if (path.Contains("07gs_geotechnology"))
                    return FileCode.GeotechnicalDesign;

                if (path.Contains("07hi_history"))
                    return FileCode.Historian;

                if (path.Contains("07ho_hospitality"))
                    return FileCode.HospitalityStrategy;

                if (path.Contains("07hp_healthcareplanning"))
                    return FileCode.HealthcareStrategy;

                if (path.Contains("07ht_hoteldesign"))
                    return FileCode.HotelDesign;

                if (path.Contains("07hw_highways"))
                    return FileCode.HighwaysStrategy;

                if (path.Contains("07hz_hazardousmaterials"))
                    return FileCode.HazardousMaterials;

                if (path.Contains("07ib_insurance"))
                    return FileCode.Insurance;

                if (path.Contains("07id_interiordesign"))
                    return FileCode.InteriorDesign;

                if (path.Contains("07int_interpretertranslator"))
                    return FileCode.Interpreter;

                if (path.Contains("07kt_kitchendesign"))
                    return FileCode.KitchenDesign;

                if (path.Contains("07la_landscape"))
                    return FileCode.LandscapeDesign;

                if (path.Contains("07lf_lifts"))
                    return FileCode.LiftStrategy;

                if (path.Contains("07lg_localinterestgroups"))
                    return FileCode.LocalInterestGroups;

                if (path.Contains("07li_lighting"))
                    return FileCode.LightingDesign;

                if (path.Contains("07lm_landscapemanager"))
                    return FileCode.LandscapeManager;

                if (path.Contains("07lo_landowner"))
                    return FileCode.Landowner;

                if (path.Contains("07lr_localresidents"))
                    return FileCode.LocalResidents;

                if (path.Contains("07lw_legal"))
                    return FileCode.LegalAgent;

                if (path.Contains("07ma_maintanenceaccess"))
                    return FileCode.MaintenanceAccessStrategy;

                if (path.Contains("07ma_masterplanning"))
                    return FileCode.Masterplanners;

                if (path.Contains("07mas_masonry"))
                    return FileCode.MasonryDesign;

                if (path.Contains("07met_mettallurgy"))
                    return FileCode.Metallurgist;

                if (path.Contains("07mm_model"))
                    return FileCode.Modelmaking;

                if (path.Contains("07mp_modelphotography"))
                    return FileCode.ModelPhotographer;

                if (path.Contains("07mt_modeltransport"))
                    return FileCode.ModelTransporter;

                if (path.Contains("07oa_officeagent"))
                    return FileCode.OfficeAgent;

                if (path.Contains("07om_ommanual"))
                    return FileCode.OMManualSpecialist;

                if (path.Contains("07pa_perspectiveartist"))
                    return FileCode.PerspectiveArtist;

                if (path.Contains("07pf_peopleflow"))
                    return FileCode.PeopleFlow;

                if (path.Contains("07ph_photography"))
                    return FileCode.Photography;

                if (path.Contains("07phc_publichealth"))
                    return FileCode.PublicHealthStrategy;

                if (path.Contains("07pl_planning"))
                    return FileCode.Planning;

                if (path.Contains("07pm_projectmanagement"))
                    return FileCode.ProjectManagement;

                if (path.Contains("07pp_publicrelations"))
                    return FileCode.PublicRelations;

                if (path.Contains("07pr_propertyconsultant"))
                    return FileCode.PropertyConsultant;

                if (path.Contains("07prm_propertymanager"))
                    return FileCode.PropertyManager;

                if (path.Contains("07pv_photovoltaics"))
                    return FileCode.Photovoltaics;

                if (path.Contains("07pw_partywall"))
                    return FileCode.PartyWallStrategy;

                if (path.Contains("07ra_retailagent"))
                    return FileCode.RetailAgent;

                if (path.Contains("07rc_retailconsutl"))
                    return FileCode.RetailConsultant;

                if (path.Contains("07resa_resiagent"))
                    return FileCode.ResidentialAgent;

                if (path.Contains("07ri_riskassessor"))
                    return FileCode.RiskConsultant;

                if (path.Contains("07rl_rightstolight"))
                    return FileCode.RightsToLightSurveyor;

                if (path.Contains("07se_security"))
                    return FileCode.SecurityStrategy;

                if (path.Contains("07sg_solarGlare"))
                    return FileCode.SolarGlareSpecialist;

                if (path.Contains("07sp_spaceplanning"))
                    return FileCode.SpacePlanning;

                if (path.Contains("07st_studiodesign"))
                    return FileCode.StudioSpecialist;

                if (path.Contains("07su_surveys"))
                    return FileCode.Surveys;

                if (path.Contains("07sus_sustainability"))
                    return FileCode.SustainabilityStrategy;

                if (path.Contains("07th_theatredesign"))
                    return FileCode.TheatreDesign;

                if (path.Contains("07tn_townscape"))
                    return FileCode.TownscapeStrategy;

                if (path.Contains("07tr_traffictransport"))
                    return FileCode.TransportStrategy;

                if (path.Contains("07ud_urbandesign"))
                    return FileCode.UrbanDesigner;

                if (path.Contains("07vis_visualisation"))
                    return FileCode.Visualisation;

                if (path.Contains("07wc_wind"))
                    return FileCode.WindStrategy;

                if (path.Contains("07wf_wayfinding"))
                    return FileCode.WayfindingDesign;

                if (path.Contains("07wr_wasterecycling"))
                    return FileCode.WasteAndRecyclingStrategy;

                if (path.Contains("07xa_executivearchitect"))
                    return FileCode.ExecutiveArchitect;
            }

            if (path.Contains("08_team"))
            {
                if (path.Contains("08c_letters"))
                    return FileCode.LettersToDesignTeam;

                if (path.Contains("08ca_consultantsappointments"))
                    return FileCode.ConsultantAppointments;

                if (path.Contains("08cc_changecontrol"))
                    return FileCode.ChangeControl;

                if (path.Contains("08dt_designteam"))
                    return FileCode.DesignTeam;

                if (path.Contains("08md_designteammeetings"))
                    return FileCode.DesignTeamMeetings;

                if (path.Contains("08pd_projectdirectory"))
                    return FileCode.ProjectDirectory;

                if (path.Contains("08pm_projectmanagement"))
                    return FileCode.ProjectManagement;

                if (path.Contains("08pr_teamprogramme"))
                    return FileCode.TeamProgramme;

                if (path.Contains("08mp_projectteammeetings"))
                    return FileCode.ProjectTeamMeetings;

                if (path.Contains("08pt_projectteam"))
                    return FileCode.ProjectTeam;

                if (path.Contains("08rm_responsibilitymatrix"))
                    return FileCode.ResponsibilityMatrix;

                if (path.Contains("08sc_subconsultants"))
                    return FileCode.Subconsultants;

                if (path.Contains("08ve_valueengineering"))
                    return FileCode.ValueEngineering;

            }

            if (path.Contains("09_technical"))
            {
                if (path.Contains("09amt"))
                    return FileCode.AMTPackage;

                if (path.Contains("09clg"))
                    return FileCode.CLGPackage;

                if (path.Contains("09ees"))
                    return FileCode.EESPackage;

                if (path.Contains("09exw"))
                    return FileCode.EXWPackage;

                if (path.Contains("09ffe"))
                    return FileCode.FFEPackage;

                if (path.Contains("09fin"))
                    return FileCode.FINPackage;

                if (path.Contains("09fls"))
                    return FileCode.FLSPackage;

                if (path.Contains("09fps"))
                    return FileCode.FPSPackage;

                if (path.Contains("09idr"))
                    return FileCode.IDRPackage;

                if (path.Contains("09ilp"))
                    return FileCode.ILPPackage;

                if (path.Contains("09lsp"))
                    return FileCode.FLSPackage;
            }

            if (path.Contains("12_maincontractor"))
            {
                if (path.Contains("12c_maincontractor"))
                    return FileCode.LettersToMainContractor;

                if (path.Contains("12sr_site"))
                    return FileCode.SiteVisitsAndReports;

                if (path.Contains("12rfi_rfi"))
                    return FileCode.RFIs;

                if (path.Contains("12cn_docs"))
                    return FileCode.OtherMainContractorDocuments;
            }

            if (path.Contains("13_specialistcontracts"))
            {
                if (path.Contains("13cd_contractordesign"))
                    return FileCode.ContractorDesign;

                if (path.Contains("13dm_demolition"))
                    return FileCode.DemolitionContractor;

                if (path.Contains("13ew_enablingworks"))
                    return FileCode.EnablingWorksContractor;

                if (path.Contains("13fo_fitout"))
                    return FileCode.FitOutContractor;

                if (path.Contains("13tc_tradecontracts"))
                    return FileCode.TradeContractors;
            }

            return FileCode.None;
        }

        public static async Task<FileCode> FindNonAdminFileCode(string path)
        {
            if (path.Contains("export"))
            {
                if (path.Contains("issue") && path.Contains("sheet"))
                    return FileCode.DocumentIssueSheet;

                if (path.Contains("issue") && path.Contains("register"))
                    return FileCode.DocumentIssueRegister;

                return
                    FileCode.DocumentIssueSheet;
            }

            if (path.Contains("import"))
            {
                return FileCode.TrackingLog;
            }

            if (path.Contains("pr"))
            {
                return FileCode.PressRelease;
            }

            if (path.Contains("wip"))
            {
                return FileCode.Sketch;
            }

            if (path.Contains("graph"))
            {
                return FileCode.Presentation;
            }

            if (path.Contains("aaminternal"))
            {
                return FileCode.DocumentForInternalCirculation;
            }

            return FileCode.None;
        }

        public static async Task<string> GuessDrawingSerialNo()
        {
            // TO DO : implement this
            return "001";
        }

        public static async Task<string> GuessDateOfIssue(string Path)
        {
            // return the date of file creation, format YYYYMMDD
            return System.IO.File.GetCreationTime(Path).ToString("yyyyMMdd");
        }

        public static async Task<TypeCode> GuessTypeCode(string Path, string Extension)
        {
            var fromExtension = await GuessTypeCodeFromExtension(Extension);
            if (fromExtension != TypeCode.None)
                return fromExtension;
            var fromPath = await GuessTypeCodeFromPath(Path);
            if (fromPath != TypeCode.None)
                return fromPath;
            var fromContent = await GuessTypeCodeFromContent();
            if (fromContent != TypeCode.None)
                return fromContent;

            return TypeCode.None;
        }

        public static async Task<TypeCode> GuessTypeCodeFromExtension(string Extension)
        {
            string[] model3dExtensions = { ".3dm", ".3ds", ".max", ".obj", ".skp", ".stl", ".fbx", ".dae", ".dwg", ".dxf", ".ifc", ".rvt", ".rfa" };

            if (model3dExtensions.Contains(Extension.ToLower()))
                return TypeCode.Sketches;

            return TypeCode.None;
        }

        public static async Task<TypeCode> GuessTypeCodeFromContent()
        {
            // TO DO : implement this
            return TypeCode.None;
        }

        public static async Task<TypeCode> GuessTypeCodeFromPath(string Path)
        {
            var path = Path.ToLower();

            if (path.Contains("sketch"))
                return TypeCode.Sketches;

            if (path.Contains("site location") || path.Contains("grid setting") || path.Contains("phasing"))
                return TypeCode.SiteLocationPlansGridSettingOutPhasingDrawings;

            if (path.Contains("schematic"))
                return TypeCode.SchematicDrawings;

            if (path.Contains("scheme presentation"))
                return TypeCode.SchemePresentationDrawings;

            if (path.Contains("existing") || path.Contains("soft-strip"))
                return TypeCode.AsExistingDrawingsSoftStripDrawings;

            if (path.Contains("general arrangement") || path.Contains("assembly") || path.Contains("ga"))
                return TypeCode.GeneralArrangementAndAssemblyDrawings;

            if (path.Contains("planning"))
                return TypeCode.PlanningApplicationDrawings;

            if (path.Contains("fire") || path.Contains("building regulation"))
                return TypeCode.FireAndBuildingRegulationDrawings;

            if (path.Contains("excavations"))
                return TypeCode.Excavations;

            if (path.Contains("floor beds"))
                return TypeCode.FloorBeds;

            if (path.Contains("foundations"))
                return TypeCode.Foundations;

            if (path.Contains("pile foundations"))
                return TypeCode.PileFoundations;

            if (path.Contains("external") || path.Contains("ees") || path.Contains("facade"))
                return TypeCode.ExternalEnvelopeSystemsEES;

            if (path.Contains("internal linings") || path.Contains("partitions") || path.Contains("ilp") || path.Contains("internal"))
                return TypeCode.InternalLiningsAndPartitionsILP;

            if (path.Contains("stairs") || path.Contains("ramps") || path.Contains("lift") || path.Contains("assembly"))
                return TypeCode.StairsRampsAndLiftShaftsAssemblyDrawings;

            if (path.Contains("roofs") || path.Contains("rfs"))
                return TypeCode.RoofsRFS;

            if (path.Contains("doors") || path.Contains("idr") || path.Contains("joinery") || path.Contains("jny"))
                return TypeCode.InternalDoorsIDRAndJoineryJNY;

            if (path.Contains("floor") || path.Contains("screeds") || path.Contains("floor finishes"))
                return TypeCode.FloorSystemsFLSScreedsSCRAndFloorFinishesFIN;

            if (path.Contains("metalwork") || path.Contains("amt"))
                return TypeCode.ArchitecturalMetalworkAMT;

            if (path.Contains("ceilings") || path.Contains("clg"))
                return TypeCode.CeilingsCLG;

            if (path.Contains("site-applied") || path.Contains("schedule"))
                return TypeCode.AllOtherSiteAppliedFinishesIncludingFinishesSchedule;

            if (path.Contains("finishes") || path.Contains("fin"))
                return TypeCode.FloorFinishesFIN;

            if (path.Contains("signage") || path.Contains("sgn"))
                return TypeCode.SignageSGN;

            if (path.Contains("furniture") || path.Contains("ffe"))
                return TypeCode.FixedFurnitureAndEquipmentFFE;

            if (path.Contains("kitchens") || path.Contains("kit"))
                return TypeCode.KitchensKIT;

            if (path.Contains("sanitary") || path.Contains("san") || path.Contains("bathroom pods") || path.Contains("pod"))
                return TypeCode.SanitarySANAndBathroomPodsPOD;

            if (path.Contains("maintenance") || path.Contains("access") || path.Contains("mae"))
                return TypeCode.MaintenanceAccessEquipmentMAE;

            if (path.Contains("loose") || path.Contains("furniture") || path.Contains("equipment"))
                return TypeCode.LooseFurnitureAndEquipment;

            if (path.Contains("external") || path.Contains("works") || path.Contains("exw"))
                return TypeCode.ExternalWorksEXW;

            return TypeCode.None;
        }

        public static async Task<Role> GuessRole(string Path)
        {
            // if the file path contains BIM, or BIM Management, or 
            // if path contains X:\AAM-WORKGROUP DATA\AAM_BIM\  but not revit and not dynamo,  then return Role.BIM

            if (Path.Contains("BIM", StringComparison.OrdinalIgnoreCase) || Path.Contains("BIM Management", StringComparison.OrdinalIgnoreCase))
            {
                if (Path.Contains("revit", StringComparison.OrdinalIgnoreCase) || Path.Contains("dynamo", StringComparison.OrdinalIgnoreCase) || Path.Contains("ARCH\\BIM") || Path.Contains("ARCH/BIM"))
                {
                    return Role.Architecture;
                }

                return Role.BIMInformationManagement;
            }

            if (Path.ToLower().Contains("landscape"))
                return Role.Landscape;

            else
                return Role.Architecture;
        }

        public static async Task<DocumentType> GuessDocumentType(string Path, string extension, string fileName)
        {
            // If filename didn't match, check file path
            if (Path.Contains("drawings", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Drawing;
            else if (Path.Contains("sketches", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Sketch;
            else if (Path.Contains("schedules", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Schedule;
            else if (Path.Contains("specifications", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Specification;
            else if (Path.Contains("animations", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Animation;
            else if (Path.Contains("architect instructions", StringComparison.OrdinalIgnoreCase))
                return DocumentType.ArchitectsInstruction;
            else if (Path.Contains("appointments", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Appointment;
            else if (Path.Contains("bids", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Bid;
            else if (Path.Contains("briefs", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Brief;
            else if (Path.Contains("correspondence", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Correspondence;
            else if (Path.Contains("certificates", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Certificate;
            else if (Path.Contains("document issues", StringComparison.OrdinalIgnoreCase))
                return DocumentType.DocumentIssue;
            else if (Path.Contains("documents", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Document;
            else if (Path.Contains("file notes", StringComparison.OrdinalIgnoreCase))
                return DocumentType.FileNotes;
            else if (Path.Contains("health and safety", StringComparison.OrdinalIgnoreCase))
                return DocumentType.HealthAndSafety;
            else if (Path.Contains("information exchange", StringComparison.OrdinalIgnoreCase))
                return DocumentType.InformationExchange;
            else if (Path.Contains("3d models", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Model3D;
            else if (Path.Contains("minutes", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Minutes;
            else if (Path.Contains("project directories", StringComparison.OrdinalIgnoreCase))
                return DocumentType.ProjectDirectory;
            else if (Path.Contains("programmes", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Programme;
            else if (Path.Contains("photos", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Photo;
            else if (Path.Contains("presentations", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Presentation;
            else if (Path.Contains("room data sheets", StringComparison.OrdinalIgnoreCase))
                return DocumentType.RoomDataSheet;
            else if (Path.Contains("requests for information", StringComparison.OrdinalIgnoreCase))
                return DocumentType.RequestForInformation;
            else if (Path.Contains("responsibility matrices", StringComparison.OrdinalIgnoreCase))
                return DocumentType.ResponsibilityMatrix;
            else if (Path.Contains("reports", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Report;
            else if (Path.Contains("trackers", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Tracker;
            else if (Path.Contains("visualisations", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Visualisation;

            if (extension.Equals(".dwg", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Drawing;
            else if (extension.Equals(".skp", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Model3D;
            else if (extension.Equals(".dgn", StringComparison.OrdinalIgnoreCase) || extension.Equals(".ai", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Drawing;
            else if (extension.Equals(".rvt", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Model3D;
            else if (extension.Equals(".ifc", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Model3D;
            else if (extension.Equals(".3dm", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Model3D;
            else if (extension.Equals(".nwc", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Model3D;
            else if (extension.Equals(".xls", StringComparison.OrdinalIgnoreCase) || extension.Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Schedule;
            else if (extension.Equals(".doc", StringComparison.OrdinalIgnoreCase) || extension.Equals(".docx", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Specification;
            else if (extension.Equals(".avi", StringComparison.OrdinalIgnoreCase) || extension.Equals(".mp4", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Animation;
            else if (extension.Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Drawing;
            else if (extension.Equals(".ics", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Appointment;
            else if (extension.Equals(".doc", StringComparison.OrdinalIgnoreCase) || extension.Equals(".docx", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Brief;
            else if (extension.Equals(".msg", StringComparison.OrdinalIgnoreCase) || extension.Equals(".eml", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Correspondence;
            else if (extension.Equals(".doc", StringComparison.OrdinalIgnoreCase) || extension.Equals(".docx", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Document;
            else if (extension.Equals(".txt", StringComparison.OrdinalIgnoreCase))
                return DocumentType.FileNotes;
            else if (extension.Equals(".fbx", StringComparison.OrdinalIgnoreCase) || extension.Equals(".obj", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Model3D;
            else if (extension.Equals(".doc", StringComparison.OrdinalIgnoreCase) || extension.Equals(".docx", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Minutes;
            else if (extension.Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Drawing;
            else if (extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) || extension.Equals(".png", StringComparison.OrdinalIgnoreCase) || extension.Equals(".psd", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Photo;
            else if (extension.Equals(".ppt", StringComparison.OrdinalIgnoreCase) || extension.Equals(".pptx", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Presentation;
            else if (extension.Equals(".doc", StringComparison.OrdinalIgnoreCase) || extension.Equals(".docx", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Report;
            else if (extension.Equals(".indd", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Report;

            else if (extension.Equals(".xls", StringComparison.OrdinalIgnoreCase) || extension.Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Tracker;
            else if (extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) || extension.Equals(".png", StringComparison.OrdinalIgnoreCase))
                return DocumentType.Visualisation;

            // If none of the conditions matched, return None
            return DocumentType.Document;
        }

        public static async Task<string> GuessProjectNumber(string path)
        {
            // X:\AAM-PROJECT DATA\802_06_CWM A1 S4-7\WORK\ARCH\BIM\1_wip  => 802_06
            // X:\AAM-PROJECT DATA\606_5KS Paddington\WORK\ARCH\BIM\1_wip => 606
            // X:\P\20103\WORK\ARCH => 20103
            // X:\P\23161\WORK\ADMIN => 23161
            // X:\P\23147_01\WORK   => 23147_01

            // split with '\'
            var parts = path.Split('\\');

            // find the first part that contains a number
            var projectNumber = parts.FirstOrDefault(p => p.Any(char.IsDigit));

            // if not found, return empty string
            if (projectNumber == null)
                return "";

            // remove all non-digit characters, but keep the underscore
            projectNumber = new string(projectNumber.Where(char.IsDigit).ToArray());

            // if the project number is empty, return empty string
            if (string.IsNullOrEmpty(projectNumber))
                return "";

            return projectNumber;
        }

        internal static async void GuessFromScratch(File file)
        {
            file.ProjectNumber = await GuessProjectNumber(file.Path);
            file.Originator = "AAM";
            file.Revision = "";
            file.DocumentType = await GuessDocumentType(file.Path, file.Extension, file.Name);
            file.Role = await GuessRole(file.Path);
            file.TypeCode = await GuessTypeCode(file.Path, file.Extension);
            file.DrawingSerialNo = await GuessDrawingSerialNo();
            file.DateOfIssue = await GuessDateOfIssue(file.Path);
            file.FileCode = await GuessFileCode(file.Path);
            file.FileType = await GuessFileType();
            (file.Description, file.Volume, file.Level) = await GuessDocumentContent(file.Name, file.Extension);
        }
    }
}
