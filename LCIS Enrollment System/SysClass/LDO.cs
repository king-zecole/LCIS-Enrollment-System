using System;
using System.Configuration;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace LCIS_Enrollment_System.SysClass
{
    class LDO
    {

        public DataSet GetMenuReport(string ProcedureName)
        {
            try
            {
                using (MySqlConnection MySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["CrimeDbConstring"].ConnectionString))
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    DataSet ds = new DataSet();

                    MySqlCommand cmd = new MySqlCommand(ProcedureName, MySqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlConnection.Open();
                    adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    MySqlConnection.Close();
                    return ds;
                }

            }
            catch
            {
                return new DataSet();
            }
        }

        public bool LogAuditTrail(string username, string type, string department, string details, string datetimes, string IP)
        {
            try
            {
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(details) && !string.IsNullOrEmpty(datetimes) && !string.IsNullOrEmpty(IP))
                {
                    using (MySqlConnection MySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["CrimeDbConstring"].ConnectionString))
                    {
                        MySqlConnection.Open();
                        string Sql = "INSERT INTO WAPIC_LIVE_BKP.HDRP_AUDIT (USERNAME, AUDITTYPE, DEPARTMENT, DETAILS, LOGDATETIME, IP) values (@USERNAME,@AUDITTYPE,@DEPARTMENT,@DETAILS,@LOGDATETIME,@IP)";
                        using (MySqlCommand cmd = new MySqlCommand(Sql, MySqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@USERNAME", username);
                            cmd.Parameters.AddWithValue("@AUDITTYPE", type);
                            cmd.Parameters.AddWithValue("@DEPARTMENT", department);
                            cmd.Parameters.AddWithValue("@DETAILS", details);
                            cmd.Parameters.AddWithValue("@LOGDATETIME", datetimes);
                            cmd.Parameters.AddWithValue("@IP", IP);
                            cmd.ExecuteNonQuery();
                        }
                        MySqlConnection.Close();
                        return true;
                    }
                }
                else
                {

                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public string assignLCISNo()
        {
            try
            {
                string NextID = string.Empty;
                string LCISNO = string.Empty;
                string tblname = ConfigurationManager.AppSettings["TBL_PROFILE"];
                using (MySqlConnection MySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["CrimeDbConstring"].ConnectionString))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    MySqlDataReader reader;
                    cmd.CommandText = "SELECT AUTO_INCREMENT FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '"+ tblname + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = MySqlConnection;
                    MySqlConnection.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string ID =  reader.GetString(0);
                        DateTime now = DateTime.Today;
                        NextID = generateLCISNumber(ID);
                        LCISNO = string.Format("LCIS/{0}{1}/{2:00000}",now.ToString("MM"), now.ToString("yy"), int.Parse(NextID));
                    }
                    MySqlConnection.Close();
                    return LCISNO;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string generateLCISNumber(string ID)
        {
            try
            {
                MD5 md5Hasher = MD5.Create();
                var hashed = md5Hasher.ComputeHash(System.Text.Encoding.UTF8.GetBytes(ID));
                var ivalue = BitConverter.ToUInt32(hashed, 0);
                return ivalue.ToString().Substring(2,2)+ID;
            }
            catch
            {
                return ID;
            }

        }

        public DataSet ViewPersonalRecords(out string Reason)
        {
            try
            {
                using (MySqlConnection MySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["CrimeDbConstring"].ConnectionString))
                {
                    MySqlConnection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    DataSet ds = new DataSet();
                    string Sql = @"SELECT `id` as 'ID', `lcis_number` as 'LCIS NO', `last_name` as 'Last Name' , `first_name` as 'First Name', `othername`  as 'OtherName', `gender` as 'Gender', `date_of_birth` as 'DOB', `country_of_orign` as 'Country', `state_of_origin` as 'State of Origin', `tribe` as 'Tribe', `religion` as 'Religion', `height_scale` as 'Height', `weight_scale` as 'Weight', `colour_of_eyes`  as 'Eye Colour', `colour_of_hair` as 'Hair Colour'
                                    FROM inmate_profile";

                    using (MySqlCommand cmd = new MySqlCommand(Sql, MySqlConnection))
                    {
                        adapter = new MySqlDataAdapter(cmd);
                        adapter.Fill(ds);

                    }

                    MySqlConnection.Close();
                    Reason = string.Empty;
                    return ds;
                }
            }
            catch (Exception ex)
            {
                Reason = "Unable to Search. " + ex.Message;
                return null;
            }
        }

        #region Get Recorded Methods

        public DataSet getPersonalInfo(string lcis_number, out string Reason)
        {
            try
            {
                using (MySqlConnection MySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["CrimeDbConstring"].ConnectionString))
                {
                    MySqlConnection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    DataSet ds = new DataSet();
                    string Sql = @"SELECT * FROM inmate_profile WHERE lcis_number = @lcis_number";

                    using (MySqlCommand cmd = new MySqlCommand(Sql, MySqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@lcis_number", lcis_number);
                        adapter = new MySqlDataAdapter(cmd);
                        adapter.Fill(ds);
                    }

                    MySqlConnection.Close();
                    Reason = string.Empty;
                    return ds;
                }
            }
            catch (Exception ex)
            {
                Reason = "Unable to Get Record. " + ex.Message;
                return null;
            }
        }

        public DataSet getPoliceInfo(string lcis_number, out string Reason)
        {
            try
            {
                using (MySqlConnection MySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["CrimeDbConstring"].ConnectionString))
                {
                    MySqlConnection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    DataSet ds = new DataSet();
                    string Sql = @"SELECT * FROM inmate_police WHERE lcis_number = @lcis_number";

                    using (MySqlCommand cmd = new MySqlCommand(Sql, MySqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@lcis_number", lcis_number);
                        adapter = new MySqlDataAdapter(cmd);
                        adapter.Fill(ds);
                    }

                    MySqlConnection.Close();
                    Reason = string.Empty;
                    return ds;
                }
            }
            catch (Exception ex)
            {
                Reason = "Unable to Get Record. " + ex.Message;
                return null;
            }
        }
        
        public DataSet getPrisonInfo(string lcis_number, out string Reason)
        {
            try
            {
                using (MySqlConnection MySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["CrimeDbConstring"].ConnectionString))
                {
                    MySqlConnection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    DataSet ds = new DataSet();
                    string Sql = @"SELECT * FROM inmate_prison WHERE lcis_number = @lcis_number";

                    using (MySqlCommand cmd = new MySqlCommand(Sql, MySqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@lcis_number", lcis_number);
                        adapter = new MySqlDataAdapter(cmd);
                        adapter.Fill(ds);
                    }

                    MySqlConnection.Close();
                    Reason = string.Empty;
                    return ds;
                }
            }
            catch (Exception ex)
            {
                Reason = "Unable to Get Record. " + ex.Message;
                return null;
            }
        }

        public DataSet getmojInfo(string lcis_number, out string Reason)
        {
            try
            {
                using (MySqlConnection MySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["CrimeDbConstring"].ConnectionString))
                {
                    MySqlConnection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    DataSet ds = new DataSet();
                    string Sql = @"SELECT * FROM inmate_moj WHERE lcis_number = @lcis_number";

                    using (MySqlCommand cmd = new MySqlCommand(Sql, MySqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@lcis_number", lcis_number);
                        adapter = new MySqlDataAdapter(cmd);
                        adapter.Fill(ds);
                    }

                    MySqlConnection.Close();
                    Reason = string.Empty;
                    return ds;
                }
            }
            catch (Exception ex)
            {
                Reason = "Unable to Get Record. " + ex.Message;
                return null;
            }
        }

        public DataSet getjudiciaryInfo(string lcis_number, out string Reason)
        {
            try
            {
                using (MySqlConnection MySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["CrimeDbConstring"].ConnectionString))
                {
                    MySqlConnection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    DataSet ds = new DataSet();
                    string Sql = @"SELECT * FROM inmate_judiciary WHERE lcis_number = @lcis_number";

                    using (MySqlCommand cmd = new MySqlCommand(Sql, MySqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@lcis_number", lcis_number);
                        adapter = new MySqlDataAdapter(cmd);
                        adapter.Fill(ds);
                    }

                    MySqlConnection.Close();
                    Reason = string.Empty;
                    return ds;
                }
            }
            catch (Exception ex)
            {
                Reason = "Unable to Get Record. " + ex.Message;
                return null;
            }
        }

        #endregion

        public DataSet SearchPersonalRecords(string schvalue, out string Reason)
        {
            try
            {
                if (!string.IsNullOrEmpty(schvalue))
                {
                    using (MySqlConnection MySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["CrimeDbConstring"].ConnectionString))
                    {
                        MySqlConnection.Open();
                        MySqlDataAdapter adapter = new MySqlDataAdapter();
                        DataSet ds = new DataSet();
                        string Sql = @"SELECT `id` as 'ID', `lcis_number` as 'LCIS NO', `last_name` as 'Last Name' , `first_name` as 'First Name', `othername`  as 'OtherName', `gender` as 'Gender', `date_of_birth` as 'DOB', `country_of_orign` as 'Country', `state_of_origin` as 'State of Origin', `tribe` as 'Tribe', `religion` as 'Religion', `height_scale` as 'Height', `weight_scale` as 'Weight', `colour_of_eyes`  as 'Eye Colour', `colour_of_hair` as 'Hair Colour'
                                        FROM inmate_profile WHERE 
                                        lcis_number LIKE @lcis_number OR
                                        last_name LIKE @last_name OR
                                        first_name LIKE @first_name OR
                                        othername LIKE @othername OR
                                        gender LIKE @gender OR
                                        address_of_defendant LIKE @address_of_defendant OR
                                        date_of_birth LIKE @date_of_birth OR
                                        country_of_orign LIKE @country_of_orign OR
                                        state_of_origin LIKE @state_of_origin OR
                                        tribe LIKE @tribe OR
                                        religion LIKE @religion OR
                                        height_scale LIKE @height_scale OR
                                        weight_scale LIKE @weight_scale OR
                                        colour_of_eyes LIKE @colour_of_eyes OR
                                        colour_of_hair LIKE @colour_of_hair OR
                                        tribal_marks LIKE @tribal_marks";

                        using (MySqlCommand cmd = new MySqlCommand(Sql, MySqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@lcis_number", "%" + schvalue + "%");
                            cmd.Parameters.AddWithValue("@last_name", "%" + schvalue + "%");
                            cmd.Parameters.AddWithValue("@first_name", "%" + schvalue + "%");
                            cmd.Parameters.AddWithValue("@othername", "%" + schvalue + "%");
                            cmd.Parameters.AddWithValue("@gender", "%" + schvalue + "%");
                            cmd.Parameters.AddWithValue("@address_of_defendant", "%" + schvalue + "%");
                            cmd.Parameters.AddWithValue("@date_of_birth", "%" + schvalue + "%");
                            cmd.Parameters.AddWithValue("@country_of_orign", "%" + schvalue + "%");
                            cmd.Parameters.AddWithValue("@state_of_origin", "%" + schvalue + "%");
                            cmd.Parameters.AddWithValue("@tribe", "%" + schvalue + "%");
                            cmd.Parameters.AddWithValue("@religion", "%" + schvalue + "%");
                            cmd.Parameters.AddWithValue("@height_scale", "%" + schvalue + "%");
                            cmd.Parameters.AddWithValue("@weight_scale", "%" + schvalue + "%");
                            cmd.Parameters.AddWithValue("@colour_of_eyes", "%" + schvalue + "%");
                            cmd.Parameters.AddWithValue("@colour_of_hair", "%" + schvalue + "%");
                            cmd.Parameters.AddWithValue("@tribal_marks", "%" + schvalue + "%");
                            adapter = new MySqlDataAdapter(cmd);
                            adapter.Fill(ds);

                        }

                        MySqlConnection.Close();
                        Reason = "Inmate Search Was Successful";
                        return ds;
                    }
                }
                else
                {
                    Reason = "Please enter a Search String";
                    return null;
                }
            }
            catch(Exception ex)
            {
                Reason = "Unable to Search. "+ex.Message;
                return null;
            }
        }

        #region Save New Records Methods

        public bool saveBasicInfo(out string Reason, string LCISNO, string last_name, string first_name, string othername, string gender, string address_of_defendant, string date_of_birth, string state_of_origin, string tribe, string religion, string height, string weight, string colour_of_eyes, string colour_of_hair, string tribal_marks, string country_of_orign = "Nigeria")
        {
            try
            {
                if (!string.IsNullOrEmpty(last_name) && !string.IsNullOrEmpty(first_name) && !string.IsNullOrEmpty(date_of_birth) && !string.IsNullOrEmpty(gender))
                {
                    using (MySqlConnection MySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["CrimeDbConstring"].ConnectionString))
                    {
                        MySqlConnection.Open();
                        string Sql = @"INSERT INTO inmate_profile (lcis_number, last_name, first_name, othername, gender, address_of_defendant, date_of_birth, country_of_orign, state_of_origin, tribe, religion, height_scale, weight_scale, colour_of_eyes, colour_of_hair, tribal_marks)
                                       values (@lcis_number, @last_name, @first_name, @othername, @gender, @address_of_defendant, @date_of_birth, @country_of_orign, @state_of_origin, @tribe, @religion, @height, @weight, @colour_of_eyes, @colour_of_hair, @tribal_marks)";
                        using (MySqlCommand cmd = new MySqlCommand(Sql, MySqlConnection))
                        {
                            DateTime rs;
                            //cmd.Parameters.AddWithValue("@SCRIPT", policynumber);
                            cmd.Parameters.AddWithValue("@lcis_number", LCISNO);
                            cmd.Parameters.AddWithValue("@last_name", last_name);
                            cmd.Parameters.AddWithValue("@first_name", first_name);
                            cmd.Parameters.AddWithValue("@othername", othername);
                            cmd.Parameters.AddWithValue("@gender", gender);
                            cmd.Parameters.AddWithValue("@address_of_defendant", address_of_defendant);
                            cmd.Parameters.AddWithValue("@date_of_birth", (DateTime.TryParse(date_of_birth, out rs)) ? rs.ToString("yyyy-MM-dd") : null);
                            cmd.Parameters.AddWithValue("@country_of_orign", country_of_orign);
                            cmd.Parameters.AddWithValue("@state_of_origin", state_of_origin);
                            cmd.Parameters.AddWithValue("@tribe", tribe);
                            cmd.Parameters.AddWithValue("@religion", religion);
                            cmd.Parameters.AddWithValue("@height", height);
                            cmd.Parameters.AddWithValue("@weight", weight);
                            cmd.Parameters.AddWithValue("@colour_of_eyes", colour_of_eyes);
                            cmd.Parameters.AddWithValue("@colour_of_hair", colour_of_hair);
                            cmd.Parameters.AddWithValue("@tribal_marks", tribal_marks);
                            cmd.ExecuteNonQuery();

                        }

                        MySqlConnection.Close();
                        Reason = "Inmate Profile Was Created Successfully. You Can Now Proceed To The Next Section.";
                        return true;
                    }
                }
                else
                {
                    Reason = "Please fill in the compulsory field's (LASTNAME, FIRSTNAME, DOB, GENDER) before Saving.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                Reason = "Unable to Create Inmate Profile. Please Try Again With a Proper Input. \nError :" + ex.Message;
                return false;
            }
        }

        public bool savePoliceInfo(out string Reason, string lcis_number, string Offence, string Name_of_Complainant, string Address_of_Complainant, string Date_Offence_Committed, string Location_offence_committed, string Date_Defendant_Arrested, string Name_of_leg_rep_defendant, string Address_of_leg_rep_defendant, string Charge_no, string Police_File_Reference, string Name_of_IPO, string Location_of_IPO, string Name_of_Police_Prosecutor, string Location_of_Police_Prosecutor, string Other_Defendants_Num, char LawyerType)
        {
            try
            {
                if (!string.IsNullOrEmpty(lcis_number) && !string.IsNullOrEmpty(Offence))
                {
                    AddOffence(Offence);
                    using (MySqlConnection MySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["CrimeDbConstring"].ConnectionString))
                    {
                        MySqlConnection.Open();
                        string Sql = @"INSERT INTO inmate_police (lcis_number, Offence, Name_of_Complainant, Address_of_Complainant, Date_Offence_Committed, Location_offence_committed, Date_Defendant_Arrested, Name_of_leg_rep_defendant, Address_of_leg_rep_defendant, Charge_no, Police_File_Reference, Name_of_IPO, Location_of_IPO, Name_of_Police_Prosecutor, Location_of_Police_Prosecutor, Other_Defendants_Num, LawyerType)
                                       values (@lcis_number, @Offence, @Name_of_Complainant, @Address_of_Complainant, @Date_Offence_Committed, @Location_offence_committed, @Date_Defendant_Arrested, @Name_of_leg_rep_defendant, @Address_of_leg_rep_defendant, @Charge_no, @Police_File_Reference, @Name_of_IPO, @Location_of_IPO, @Name_of_Police_Prosecutor, @Location_of_Police_Prosecutor, @Other_Defendants_Num, @LawyerType)";
                        using (MySqlCommand cmd = new MySqlCommand(Sql, MySqlConnection))
                        {
                            DateTime rs;
                            cmd.Parameters.AddWithValue("@lcis_number", lcis_number);
                            cmd.Parameters.AddWithValue("@Offence", Offence);
                            cmd.Parameters.AddWithValue("@Name_of_Complainant", Name_of_Complainant);
                            cmd.Parameters.AddWithValue("@Address_of_Complainant", Address_of_Complainant);
                            cmd.Parameters.AddWithValue("@Date_Offence_Committed", (DateTime.TryParse(Date_Offence_Committed, out rs)) ? rs.ToString("yyyy-MM-dd") : null);
                            cmd.Parameters.AddWithValue("@Location_offence_committed", Location_offence_committed);
                            cmd.Parameters.AddWithValue("@Date_Defendant_Arrested", (DateTime.TryParse(Date_Defendant_Arrested, out rs)) ? rs.ToString("yyyy-MM-dd") : null);
                            cmd.Parameters.AddWithValue("@Name_of_leg_rep_defendant", Name_of_leg_rep_defendant);
                            cmd.Parameters.AddWithValue("@Address_of_leg_rep_defendant", Address_of_leg_rep_defendant);
                            cmd.Parameters.AddWithValue("@Charge_no", Charge_no);
                            cmd.Parameters.AddWithValue("@Police_File_Reference", Police_File_Reference);
                            cmd.Parameters.AddWithValue("@Name_of_IPO", Name_of_IPO);
                            cmd.Parameters.AddWithValue("@Location_of_IPO", Location_of_IPO);
                            cmd.Parameters.AddWithValue("@Name_of_Police_Prosecutor", Name_of_Police_Prosecutor);
                            cmd.Parameters.AddWithValue("@Location_of_Police_Prosecutor", Location_of_Police_Prosecutor);
                            cmd.Parameters.AddWithValue("@Other_Defendants_Num", Other_Defendants_Num);
                            cmd.Parameters.AddWithValue("@LawyerType", LawyerType);
                            cmd.ExecuteNonQuery();

                        }

                        MySqlConnection.Close();
                        Reason = "Inmate Police Information Was Saved Successfully. You Can Now Proceed To The Next Section.";
                        return true;
                    }
                }
                else
                {
                    Reason = "Please fill in the compulsory field's (OFFENCE) before Saving.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                Reason = "Unable to Save Inmate Police Information. Please Try Again With a Proper Input. \nError :" + ex.Message;
                return false;
            }
        }

        public bool saveMOJInfo(out string Reason, string lcis_number, string Offence, string Sub_offence, string DPP_Ref_Number, string Date_file_received_MOJ, string Date_add_request_police, string accused_status, string Last_adjourned_date, string next_hearing_date)
        {
            try
            {
                if (!string.IsNullOrEmpty(lcis_number) && !string.IsNullOrEmpty(Offence))
                {
                    AddOffence(Offence);
                    using (MySqlConnection MySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["CrimeDbConstring"].ConnectionString))
                    {
                        MySqlConnection.Open();
                        string Sql = @"INSERT INTO inmate_moj (lcis_number, Offence, Sub_offence, DPP_Ref_Number, Date_file_received_MOJ, Date_add_request_police, accused_status, Last_adjourned_date, next_hearing_date)
                                       values (@lcis_number, @Offence, @Sub_offence, @DPP_Ref_Number, @Date_file_received_MOJ, @Date_add_request_police, @accused_status, @Last_adjourned_date, @next_hearing_date)";
                        using (MySqlCommand cmd = new MySqlCommand(Sql, MySqlConnection))
                        {

                            DateTime rs;
                            cmd.Parameters.AddWithValue("@lcis_number", lcis_number);
                            cmd.Parameters.AddWithValue("@Offence", Offence);
                            cmd.Parameters.AddWithValue("@Sub_offence", Sub_offence);
                            cmd.Parameters.AddWithValue("@DPP_Ref_Number", DPP_Ref_Number);
                            cmd.Parameters.AddWithValue("@Date_file_received_MOJ", (DateTime.TryParse(Date_file_received_MOJ, out rs)) ? rs.ToString("yyyy-MM-dd") : null);
                            cmd.Parameters.AddWithValue("@Date_add_request_police", (DateTime.TryParse(Date_add_request_police, out rs)) ? rs.ToString("yyyy-MM-dd") : null);
                            cmd.Parameters.AddWithValue("@accused_status", accused_status);
                            cmd.Parameters.AddWithValue("@Last_adjourned_date", (DateTime.TryParse(Last_adjourned_date, out rs)) ? rs.ToString("yyyy-MM-dd") : null);
                            cmd.Parameters.AddWithValue("@next_hearing_date", (DateTime.TryParse(next_hearing_date, out rs)) ? rs.ToString("yyyy-MM-dd") : null);
                            cmd.ExecuteNonQuery();

                        }

                        MySqlConnection.Close();
                        Reason = "Inmate Ministry Of Justice Information Was Saved Successfully. You Can Now Proceed To The Next Section.";
                        return true;
                    }
                }
                else
                {
                    Reason = "Please fill in the compulsory field's (OFFENCE) before Saving.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                Reason = "Unable to Save Inmate Ministry Of Justice Information. Please Try Again With a Proper Input. \nError :" + ex.Message;
                return false;
            }
        }

        public bool saveJudisciaryInfo(out string Reason, string lcis_number, string Trial_Court, string Previous_Judge_Magistrate, string Magistrate_Court_Name_No, string High_Court_Name_No, string Mgt_Court_Reference, string High_Court_Reference, string Last_adjourned_date, string next_hearing_date)
        {
            try
            {
                if (!string.IsNullOrEmpty(lcis_number) && !string.IsNullOrEmpty(Trial_Court))
                {
                    using (MySqlConnection MySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["CrimeDbConstring"].ConnectionString))
                    {
                        MySqlConnection.Open();
                        string Sql = @"INSERT INTO inmate_judiciary (lcis_number, Trial_Court, Previous_Judge_Magistrate, Magistrate_Court_Name_No, High_Court_Name_No, Mgt_Court_Reference, High_Court_Reference, Last_adjourned_date, next_hearing_date)
                                       values (@lcis_number, @Trial_Court, @Previous_Judge_Magistrate, @Magistrate_Court_Name_No, @High_Court_Name_No, @Mgt_Court_Reference, @High_Court_Reference, @Last_adjourned_date, @next_hearing_date)";
                        using (MySqlCommand cmd = new MySqlCommand(Sql, MySqlConnection))
                        {
                            DateTime rs;
                            cmd.Parameters.AddWithValue("@lcis_number", lcis_number);
                            cmd.Parameters.AddWithValue("@Trial_Court", Trial_Court);
                            cmd.Parameters.AddWithValue("@Previous_Judge_Magistrate", Previous_Judge_Magistrate);
                            cmd.Parameters.AddWithValue("@Magistrate_Court_Name_No", Magistrate_Court_Name_No);
                            cmd.Parameters.AddWithValue("@High_Court_Name_No", High_Court_Name_No);
                            cmd.Parameters.AddWithValue("@Mgt_Court_Reference", Mgt_Court_Reference);
                            cmd.Parameters.AddWithValue("@High_Court_Reference", High_Court_Reference);
                            cmd.Parameters.AddWithValue("@Last_adjourned_date", (DateTime.TryParse(Last_adjourned_date, out rs))? rs.ToString("yyyy-MM-dd"): null);
                            cmd.Parameters.AddWithValue("@next_hearing_date", (DateTime.TryParse(next_hearing_date, out rs)) ? rs.ToString("yyyy-MM-dd") : null);
                            cmd.ExecuteNonQuery();

                        }

                        MySqlConnection.Close();
                        Reason = "Inmate Judiciary Information Was Saved Successfully. You Can Now Proceed To The Next Section.";
                        return true;
                    }
                }
                else
                {
                    Reason = "Please fill in the compulsory field's (LOCATION OF TRIAL COURT) before Saving.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                Reason = "Unable to Save Inmate Judiciary Information. Please Try Again With a Proper Input. \nError :" + ex.Message;
                return false;
            }
        }

        public bool savePrisonInfo(out string Reason, string lcis_number, string Prison_name, string Prisoner_No, string Inmate_Category, string Additional_Status, int IsDppAdviceReady, string TermEndDate, string prison_yard, string Date_admission, string Additional_info_enforce_agency, string Organization, string Reference_No)
        {
            try
            {
                if (!string.IsNullOrEmpty(lcis_number) && !string.IsNullOrEmpty(Prison_name) && !string.IsNullOrEmpty(Prisoner_No) && !string.IsNullOrEmpty(Inmate_Category) && !string.IsNullOrEmpty(Date_admission))
                {
                    using (MySqlConnection MySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["CrimeDbConstring"].ConnectionString))
                    {
                        MySqlConnection.Open();
                        string Sql = @"INSERT INTO inmate_prison (lcis_number, Prison_name, Prisoner_No, Inmate_Category, Additional_Status, IsDppAdviceReady, TermEndDate, prison_yard, Date_admission, Additional_info_enforce_agency, Organization, Reference_No)
                                       values (@lcis_number, @Prison_name, @Prisoner_No, @Inmate_Category, @Additional_Status, @IsDppAdviceReady, @TermEndDate, @prison_yard, @Date_admission, @Additional_info_enforce_agency, @Organization, @Reference_No)";
                        using (MySqlCommand cmd = new MySqlCommand(Sql, MySqlConnection))
                        {

                            DateTime rs;
                            cmd.Parameters.AddWithValue("@lcis_number", lcis_number);
                            cmd.Parameters.AddWithValue("@Prison_name", Prison_name);
                            cmd.Parameters.AddWithValue("@Prisoner_No", Prisoner_No);
                            cmd.Parameters.AddWithValue("@Inmate_Category", Inmate_Category);
                            cmd.Parameters.AddWithValue("@Additional_Status", Additional_Status);
                            cmd.Parameters.AddWithValue("@IsDppAdviceReady", IsDppAdviceReady);
                            cmd.Parameters.AddWithValue("@TermEndDate", (DateTime.TryParse(TermEndDate, out rs)) ? rs.ToString("yyyy-MM-dd") : null);
                            cmd.Parameters.AddWithValue("@prison_yard", prison_yard);
                            cmd.Parameters.AddWithValue("@Date_admission", (DateTime.TryParse(Date_admission, out rs)) ? rs.ToString("yyyy-MM-dd") : null);
                            cmd.Parameters.AddWithValue("@Additional_info_enforce_agency", Additional_info_enforce_agency);
                            cmd.Parameters.AddWithValue("@Organization", Organization);
                            cmd.Parameters.AddWithValue("@Reference_No", Reference_No);
                            cmd.ExecuteNonQuery();

                        }

                        MySqlConnection.Close();
                        Reason = "Inmate Prison Information Was Saved Successfully. You Can Now Proceed To The Next Section.";
                        return true;
                    }
                }
                else
                {
                    Reason = "Please fill in the compulsory field's (PRISON NAME, INMATE PRISON NUMBER, INMATE CATEGORY, DATE OF FIRST ADMISSION) before Saving.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                Reason = "Unable to Save Inmate Prison Information. Please Try Again With a Proper Input. \nError :" + ex.Message;
                return false;
            }
        }

        public bool saveInmateImage(out string Reason, string lcis_number, byte[] Photograph)
        {
            try
            {
                if (!string.IsNullOrEmpty(lcis_number) && (Photograph != null))
                {
                    using (MySqlConnection MySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["CrimeDbConstring"].ConnectionString))
                    {
                        MySqlConnection.Open();
                        string Sql = @"UPDATE inmate_prison SET Photograph=@Photograph WHERE lower(lcis_number)=@lcis_number";
                        using (MySqlCommand cmd = new MySqlCommand(Sql, MySqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@lcis_number", lcis_number);
                            cmd.Parameters.AddWithValue("@Photograph", Photograph);
                            if (cmd.ExecuteNonQuery() < 1)
                            {
                                Reason = "Please fill in the inmate prison information, before capturing images.";
                                return false;
                            }

                        }

                        MySqlConnection.Close();
                        Reason = "Inmate Picture Was Saved Successfully. Please Proceed to FingerPrint Capturing Section.";
                        return true;
                    }
                }
                else
                {
                    Reason = "Please Capture The Inmate Picture before Saving, Or LCIS Number is Missing.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                Reason = "Unable to Save Inmate Picture. Please Try Again. \nError :" + ex.Message;
                return false;
            }
        }

        public bool saveInmateFingerPrint(out string Reason, string lcis_number, byte[] Fingerprint_Thumb, byte[] Fingerprint_FLeft, byte[] Fingerprint_FRight)
        {
            try
            {
                if (!string.IsNullOrEmpty(lcis_number) && (Fingerprint_Thumb != null) && (Fingerprint_FLeft != null) && (Fingerprint_FRight != null))
                {
                    using (MySqlConnection MySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["CrimeDbConstring"].ConnectionString))
                    {
                        MySqlConnection.Open();
                        string Sql = @"UPDATE inmate_prison SET Fingerprint_Thumb=@Fingerprint_Thumb, Fingerprint_FLeft=@Fingerprint_FLeft, Fingerprint_FRight=@Fingerprint_FRight WHERE lower(lcis_number)=@lcis_number";
                        using (MySqlCommand cmd = new MySqlCommand(Sql, MySqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@lcis_number", lcis_number);
                            cmd.Parameters.AddWithValue("@Fingerprint_Thumb", Fingerprint_Thumb);
                            cmd.Parameters.AddWithValue("@Fingerprint_FLeft", Fingerprint_FLeft);
                            cmd.Parameters.AddWithValue("@Fingerprint_FRight", Fingerprint_FRight);

                            if (cmd.ExecuteNonQuery() < 1)
                            {
                                Reason = "Please fill in the inmate prison information, before capturing the fingerprint.";
                                return false;
                            }

                        }

                        MySqlConnection.Close();
                        Reason = "Inmate FingerPrint Was Captured & Saved Successfully. You Can Proceed to Enroll a New Inmate.";
                        return true;
                    }
                }
                else
                {
                    Reason = "Please Capture The Inmate FingerPrint before Saving, Or LCIS Number is Missing.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                Reason = "Unable to Save Inmate Biometric Please Try Again. \nError :" + ex.Message;
                return false;
            }
        }

        #endregion

        public void AddOffence(string Offence)
        {
            if (!string.IsNullOrEmpty(Offence))
            {
                try
                {
                    if (!IsOffenceExist(Offence))
                    {
                        using (MySqlConnection MySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["CrimeDbConstring"].ConnectionString))
                        {
                            MySqlConnection.Open();
                            string Sql = @"INSERT INTO offences (name) values ( @Offence)";
                            using (MySqlCommand cmd = new MySqlCommand(Sql, MySqlConnection))
                            {
                                cmd.Parameters.AddWithValue("@Offence", Offence);
                                cmd.ExecuteNonQuery();
                            }
                            MySqlConnection.Close();
                        }
                    }
                }
                catch { }
            }
        }

        public bool IsOffenceExist(string Offence)
        {
            try
            {
                bool valid = true;
                if (!string.IsNullOrEmpty(Offence))
                {
                    using (MySqlConnection MySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["CrimeDbConstring"].ConnectionString))
                    {
                        MySqlCommand cmd = new MySqlCommand();
                        MySqlDataReader reader;
                        cmd.CommandText = "SELECT * FROM OFFENCES WHERE lower(name)='" + Offence.ToLower() + "'";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = MySqlConnection;
                        MySqlConnection.Open();
                        reader = cmd.ExecuteReader();
                        valid = reader.HasRows;
                        MySqlConnection.Close();
                        return valid;
                    }
                }
                return valid;

            }
            catch (Exception ex)
            {
                return true;
            }
        }

        #region Edit Records Methods

        public bool EditBasicInfo(out string Reason, string LCISNO, string last_name, string first_name, string othername, string gender, string address_of_defendant, string date_of_birth, string state_of_origin, string tribe, string religion, string height, string weight, string colour_of_eyes, string colour_of_hair, string tribal_marks, string country_of_orign = "Nigeria")
        {
            try
            {
                if (!string.IsNullOrEmpty(last_name) && !string.IsNullOrEmpty(first_name) && !string.IsNullOrEmpty(date_of_birth) && !string.IsNullOrEmpty(gender))
                {
                    using (MySqlConnection MySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["CrimeDbConstring"].ConnectionString))
                    {
                        MySqlConnection.Open();
                        string Sql = @"UPDATE inmate_profile SET last_name = @last_name, first_name = @first_name, othername = @othername, gender = @gender, address_of_defendant = @address_of_defendant, date_of_birth = @date_of_birth, country_of_orign = @country_of_orign, state_of_origin = @state_of_origin, tribe = @tribe, religion = @religion, height_scale = @height, weight_scale = @weight, colour_of_eyes = @colour_of_eyes, colour_of_hair = @colour_of_hair, tribal_marks = @tribal_marks
                                       WHERE lcis_number=@lcis_number ";

                        using (MySqlCommand cmd = new MySqlCommand(Sql, MySqlConnection))
                        {
                            DateTime rs;
                            cmd.Parameters.AddWithValue("@lcis_number", LCISNO);
                            cmd.Parameters.AddWithValue("@last_name", last_name);
                            cmd.Parameters.AddWithValue("@first_name", first_name);
                            cmd.Parameters.AddWithValue("@othername", othername);
                            cmd.Parameters.AddWithValue("@gender", gender);
                            cmd.Parameters.AddWithValue("@address_of_defendant", address_of_defendant);
                            cmd.Parameters.AddWithValue("@date_of_birth", (DateTime.TryParse(date_of_birth, out rs)) ? rs.ToString("yyyy-MM-dd") : null);
                            cmd.Parameters.AddWithValue("@country_of_orign", country_of_orign);
                            cmd.Parameters.AddWithValue("@state_of_origin", state_of_origin);
                            cmd.Parameters.AddWithValue("@tribe", tribe);
                            cmd.Parameters.AddWithValue("@religion", religion);
                            cmd.Parameters.AddWithValue("@height", height);
                            cmd.Parameters.AddWithValue("@weight", weight);
                            cmd.Parameters.AddWithValue("@colour_of_eyes", colour_of_eyes);
                            cmd.Parameters.AddWithValue("@colour_of_hair", colour_of_hair);
                            cmd.Parameters.AddWithValue("@tribal_marks", tribal_marks);
                            cmd.ExecuteNonQuery();

                        }

                        MySqlConnection.Close();
                        Reason = "Inmate Personal Profile Was Modify Successfully.";
                        return true;
                    }
                }
                else
                {
                    Reason = "Please fill in the compulsory field's (LASTNAME, FIRSTNAME, DOB, GENDER) before Saving.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                Reason = "Unable to Edit Inmate Profile. Please Try Again With a Proper Input. \nError :" + ex.Message;
                return false;
            }
        }

        public bool EditPoliceInfo(out string Reason, string lcis_number, string Offence, string Name_of_Complainant, string Address_of_Complainant, string Date_Offence_Committed, string Location_offence_committed, string Date_Defendant_Arrested, string Name_of_leg_rep_defendant, string Address_of_leg_rep_defendant, string Charge_no, string Police_File_Reference, string Name_of_IPO, string Location_of_IPO, string Name_of_Police_Prosecutor, string Location_of_Police_Prosecutor, string Other_Defendants_Num, char LawyerType)
        {
            try
            {
                if (!string.IsNullOrEmpty(lcis_number) && !string.IsNullOrEmpty(Offence))
                {
                    AddOffence(Offence);
                    using (MySqlConnection MySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["CrimeDbConstring"].ConnectionString))
                    {
                        MySqlConnection.Open();
                        string Sql = @"UPDATE inmate_police SET Offence = @Offence, Name_of_Complainant = @Name_of_Complainant, Address_of_Complainant = @Address_of_Complainant, Date_Offence_Committed = @Date_Offence_Committed, Location_offence_committed = @Location_offence_committed, Date_Defendant_Arrested = @Date_Defendant_Arrested, Name_of_leg_rep_defendant = @Name_of_leg_rep_defendant, Address_of_leg_rep_defendant = @Address_of_leg_rep_defendant, Charge_no = @Charge_no, Police_File_Reference = @Police_File_Reference, Name_of_IPO = @Name_of_IPO, Location_of_IPO = @Location_of_IPO, Name_of_Police_Prosecutor = @Name_of_Police_Prosecutor, Location_of_Police_Prosecutor = @Location_of_Police_Prosecutor, Other_Defendants_Num = @Other_Defendants_Num, LawyerType =@LawyerType 
                                        WHERE lcis_number=@lcis_number ";

                        using (MySqlCommand cmd = new MySqlCommand(Sql, MySqlConnection))
                        {
                            DateTime rs;
                            cmd.Parameters.AddWithValue("@lcis_number", lcis_number);
                            cmd.Parameters.AddWithValue("@Offence", Offence);
                            cmd.Parameters.AddWithValue("@Name_of_Complainant", Name_of_Complainant);
                            cmd.Parameters.AddWithValue("@Address_of_Complainant", Address_of_Complainant);
                            cmd.Parameters.AddWithValue("@Date_Offence_Committed", (DateTime.TryParse(Date_Offence_Committed, out rs)) ? rs.ToString("yyyy-MM-dd") : null);
                            cmd.Parameters.AddWithValue("@Location_offence_committed", Location_offence_committed);
                            cmd.Parameters.AddWithValue("@Date_Defendant_Arrested", (DateTime.TryParse(Date_Defendant_Arrested, out rs)) ? rs.ToString("yyyy-MM-dd") : null);
                            cmd.Parameters.AddWithValue("@Name_of_leg_rep_defendant", Name_of_leg_rep_defendant);
                            cmd.Parameters.AddWithValue("@Address_of_leg_rep_defendant", Address_of_leg_rep_defendant);
                            cmd.Parameters.AddWithValue("@Charge_no", Charge_no);
                            cmd.Parameters.AddWithValue("@Police_File_Reference", Police_File_Reference);
                            cmd.Parameters.AddWithValue("@Name_of_IPO", Name_of_IPO);
                            cmd.Parameters.AddWithValue("@Location_of_IPO", Location_of_IPO);
                            cmd.Parameters.AddWithValue("@Name_of_Police_Prosecutor", Name_of_Police_Prosecutor);
                            cmd.Parameters.AddWithValue("@Location_of_Police_Prosecutor", Location_of_Police_Prosecutor);
                            cmd.Parameters.AddWithValue("@Other_Defendants_Num", Other_Defendants_Num);
                            cmd.Parameters.AddWithValue("@LawyerType", LawyerType);
                            cmd.ExecuteNonQuery();

                        }

                        MySqlConnection.Close();
                        Reason = "Inmate Police Information Was Modify Successfully.";
                        return true;
                    }
                }
                else
                {
                    Reason = "Please fill in the compulsory field's (OFFENCE) before Saving.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                Reason = "Unable to Edit Inmate Police Information. Please Try Again With a Proper Input. \nError :" + ex.Message;
                return false;
            }
        }

        public bool EditMOJInfo(out string Reason, string lcis_number, string Offence, string Sub_offence, string DPP_Ref_Number, string Date_file_received_MOJ, string Date_add_request_police, string accused_status, string Last_adjourned_date, string next_hearing_date)
        {
            try
            {
                if (!string.IsNullOrEmpty(lcis_number) && !string.IsNullOrEmpty(Offence))
                {
                    AddOffence(Offence);
                    using (MySqlConnection MySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["CrimeDbConstring"].ConnectionString))
                    {
                        MySqlConnection.Open();
                        string Sql = @"UPDATE inmate_moj SET Offence = @Offence, Sub_offence = @Sub_offence, DPP_Ref_Number = @DPP_Ref_Number, Date_file_received_MOJ = @Date_file_received_MOJ, Date_add_request_police = @Date_add_request_police, accused_status = @accused_status, Last_adjourned_date = @Last_adjourned_date, next_hearing_date = @next_hearing_date
                                        WHERE lcis_number=@lcis_number ";

                        using (MySqlCommand cmd = new MySqlCommand(Sql, MySqlConnection))
                        {

                            DateTime rs;
                            cmd.Parameters.AddWithValue("@lcis_number", lcis_number);
                            cmd.Parameters.AddWithValue("@Offence", Offence);
                            cmd.Parameters.AddWithValue("@Sub_offence", Sub_offence);
                            cmd.Parameters.AddWithValue("@DPP_Ref_Number", DPP_Ref_Number);
                            cmd.Parameters.AddWithValue("@Date_file_received_MOJ", (DateTime.TryParse(Date_file_received_MOJ, out rs)) ? rs.ToString("yyyy-MM-dd") : null);
                            cmd.Parameters.AddWithValue("@Date_add_request_police", (DateTime.TryParse(Date_add_request_police, out rs)) ? rs.ToString("yyyy-MM-dd") : null);
                            cmd.Parameters.AddWithValue("@accused_status", accused_status);
                            cmd.Parameters.AddWithValue("@Last_adjourned_date", (DateTime.TryParse(Last_adjourned_date, out rs)) ? rs.ToString("yyyy-MM-dd") : null);
                            cmd.Parameters.AddWithValue("@next_hearing_date", (DateTime.TryParse(next_hearing_date, out rs)) ? rs.ToString("yyyy-MM-dd") : null);
                            cmd.ExecuteNonQuery();

                        }

                        MySqlConnection.Close();
                        Reason = "Inmate Ministry Of Justice Information Was Modify Successfully.";
                        return true;
                    }
                }
                else
                {
                    Reason = "Please fill in the compulsory field's (OFFENCE) before Saving.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                Reason = "Unable to Edit Inmate Ministry Of Justice Information. Please Try Again With a Proper Input. \nError :" + ex.Message;
                return false;
            }
        }

        public bool EditJudisciaryInfo(out string Reason, string lcis_number, string Trial_Court, string Previous_Judge_Magistrate, string Magistrate_Court_Name_No, string High_Court_Name_No, string Mgt_Court_Reference, string High_Court_Reference, string Last_adjourned_date, string next_hearing_date)
        {
            try
            {
                if (!string.IsNullOrEmpty(lcis_number) && !string.IsNullOrEmpty(Trial_Court))
                {
                    using (MySqlConnection MySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["CrimeDbConstring"].ConnectionString))
                    {
                        MySqlConnection.Open();
                        string Sql = @"UPDATE inmate_judiciary SET Trial_Court = @Trial_Court, Previous_Judge_Magistrate = @Previous_Judge_Magistrate, Magistrate_Court_Name_No = @Magistrate_Court_Name_No, High_Court_Name_No = @High_Court_Name_No, Mgt_Court_Reference = @Mgt_Court_Reference, High_Court_Reference = @High_Court_Reference, Last_adjourned_date = @Last_adjourned_date, next_hearing_date = @next_hearing_date
                                       WHERE lcis_number=@lcis_number ";

                        using (MySqlCommand cmd = new MySqlCommand(Sql, MySqlConnection))
                        {
                            DateTime rs;
                            cmd.Parameters.AddWithValue("@lcis_number", lcis_number);
                            cmd.Parameters.AddWithValue("@Trial_Court", Trial_Court);
                            cmd.Parameters.AddWithValue("@Previous_Judge_Magistrate", Previous_Judge_Magistrate);
                            cmd.Parameters.AddWithValue("@Magistrate_Court_Name_No", Magistrate_Court_Name_No);
                            cmd.Parameters.AddWithValue("@High_Court_Name_No", High_Court_Name_No);
                            cmd.Parameters.AddWithValue("@Mgt_Court_Reference", Mgt_Court_Reference);
                            cmd.Parameters.AddWithValue("@High_Court_Reference", High_Court_Reference);
                            cmd.Parameters.AddWithValue("@Last_adjourned_date", (DateTime.TryParse(Last_adjourned_date, out rs)) ? rs.ToString("yyyy-MM-dd") : null);
                            cmd.Parameters.AddWithValue("@next_hearing_date", (DateTime.TryParse(next_hearing_date, out rs)) ? rs.ToString("yyyy-MM-dd") : null);
                            cmd.ExecuteNonQuery();

                        }

                        MySqlConnection.Close();
                        Reason = "Inmate Judiciary Information Was Modify Successfully.";
                        return true;
                    }
                }
                else
                {
                    Reason = "Please fill in the compulsory field's (LOCATION OF TRIAL COURT) before Saving.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                Reason = "Unable to Edit Inmate Judiciary Information. Please Try Again With a Proper Input. \nError :" + ex.Message;
                return false;
            }
        }

        public bool EditPrisonInfo(out string Reason, string lcis_number, string Prison_name, string Prisoner_No, string Inmate_Category, string Additional_Status, int IsDppAdviceReady, string TermEndDate, string prison_yard, string Date_admission, string Additional_info_enforce_agency, string Organization, string Reference_No)
        {
            try
            {
                if (!string.IsNullOrEmpty(lcis_number) && !string.IsNullOrEmpty(Prison_name) && !string.IsNullOrEmpty(Prisoner_No) && !string.IsNullOrEmpty(Inmate_Category) && !string.IsNullOrEmpty(Date_admission))
                {
                    using (MySqlConnection MySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["CrimeDbConstring"].ConnectionString))
                    {
                        MySqlConnection.Open();
                        string Sql = @"UPDATE inmate_prison SET Prison_name = @Prison_name, Prisoner_No = @Prisoner_No, Inmate_Category = @Inmate_Category, Additional_Status = @Additional_Status, IsDppAdviceReady = @IsDppAdviceReady, TermEndDate = @TermEndDate, prison_yard = @prison_yard, Date_admission = @Date_admission, Additional_info_enforce_agency = @Additional_info_enforce_agency, Organization = @Organization, Reference_No = @Reference_No
                                       WHERE lcis_number=@lcis_number ";

                        using (MySqlCommand cmd = new MySqlCommand(Sql, MySqlConnection))
                        {
                            DateTime rs;
                            cmd.Parameters.AddWithValue("@lcis_number", lcis_number);
                            cmd.Parameters.AddWithValue("@Prison_name", Prison_name);
                            cmd.Parameters.AddWithValue("@Prisoner_No", Prisoner_No);
                            cmd.Parameters.AddWithValue("@Inmate_Category", Inmate_Category);
                            cmd.Parameters.AddWithValue("@Additional_Status", Additional_Status);
                            cmd.Parameters.AddWithValue("@IsDppAdviceReady", IsDppAdviceReady);
                            cmd.Parameters.AddWithValue("@TermEndDate", (DateTime.TryParse(TermEndDate, out rs)) ? rs.ToString("yyyy-MM-dd") : null);
                            cmd.Parameters.AddWithValue("@prison_yard", prison_yard);
                            cmd.Parameters.AddWithValue("@Date_admission", (DateTime.TryParse(Date_admission, out rs)) ? rs.ToString("yyyy-MM-dd") : null);
                            cmd.Parameters.AddWithValue("@Additional_info_enforce_agency", Additional_info_enforce_agency);
                            cmd.Parameters.AddWithValue("@Organization", Organization);
                            cmd.Parameters.AddWithValue("@Reference_No", Reference_No);
                            cmd.ExecuteNonQuery();

                        }

                        MySqlConnection.Close();
                        Reason = "Inmate Prison Information Was Modify Successfully.";
                        return true;
                    }
                }
                else
                {
                    Reason = "Please fill in the compulsory field's (PRISON NAME, INMATE PRISON NUMBER, INMATE CATEGORY, DATE OF FIRST ADMISSION) before Saving.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                Reason = "Unable to Edit Inmate Prison Information. Please Try Again With a Proper Input. \nError :" + ex.Message;
                return false;
            }
        }
        
        #endregion

    }
}
