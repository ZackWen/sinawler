using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Sinawler.Model
{
	/// <summary>
    /// ��Status�������еķ��������������ݿ⽻�������ж�ȡ��������д������
	/// </summary>
    /// 
    /*
     <?xml version="1.0" encoding="UTF-8"?>
        <status>
            <created_at>Fri Sep 03 11:41:08 +0800 2010</created_at>
            <id>2265657555</id>
            <text>��������س��п�����Ȼ����ͬѧ��Է�����Ϊ�������ȥ�����ˣ��������͡�ϣ����������D����ȵ��Ҿ�ѵʱ��������</text>
            <source>
                <a href="http://t.sina.com.cn">����΢��</a>
            </source>
            <favorited>false</favorited>
            <truncated>false</truncated>
            <geo/>
            <in_reply_to_status_id/>
            <in_reply_to_user_id/>
            <in_reply_to_screen_name/>
            <user>
                <id>1763124584</id>
                <screen_name>��-��</screen_name>
                <name>��-��</name>
                <province>44</province>
                <city>1</city>
                <location>�㶫 ����</location>
                <description>��˧Ҳ��C</description>
                <url>http://blog.163.com/chen_mou/</url>
                <profile_image_url>http://tp1.sinaimg.cn/1763124584/50/1283605697</profile_image_url>
                <domain/>
                <gender>m</gender>
                <followers_count>17</followers_count>
                <friends_count>16</friends_count>
                <statuses_count>71</statuses_count>
                <favourites_count>0</favourites_count>
                <created_at>Thu Jun 17 00:00:00 +0800 2010</created_at>
                <following>false</following>
                <verified>false</verified>
                <allow_all_act_msg>false</allow_all_act_msg>
                <geo_enabled>false</geo_enabled>
            </user>
        </status>
    */

    public class Status
	{
        public Status()
        { }

		#region Model
		private long _status_id;
		private string _created_at;
		private string _content;
		private string _source_url;
		private string _source_name;
		private bool _favorited;
		private bool _truncated;
        private string _geo;
		private long _in_reply_to_status_id;
		private long _in_reply_to_user_id;
		private string _in_reply_to_screen_name;
		private string _thumbnail_pic="";
		private string _bmiddle_pic="";
		private string _original_pic="";
		private long _user_id;
        private Status _retweeted_status = null;
        private int _iteration;
        private string _update_time;
		/// <summary>
		/// ΢��ID��XML��Ϊid��
		/// </summary>
		public long status_id
		{
			set{ _status_id=value;}
			get{return _status_id;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public string created_at
		{
			set{ _created_at=value;}
			get{return _created_at;}
		}
		/// <summary>
		/// ΢����Ϣ����(XML��Ϊtext)
		/// </summary>
		public string content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// ΢����Դ�е�URL
		/// </summary>
		public string source_url
		{
			set{ _source_url=value;}
			get{return _source_url;}
		}
		/// <summary>
		/// ΢����Դ������
		/// </summary>
		public string source_name
		{
			set{ _source_name=value;}
			get{return _source_name;}
		}
		/// <summary>
		/// �Ƿ����ղأ�Ӧ���Ƕ��ڵ�ǰ��¼�˺ţ�
		/// </summary>
		public bool favorited
		{
			set{ _favorited=value;}
			get{return _favorited;}
		}
		/// <summary>
		/// �Ƿ񱻽ض�
		/// </summary>
		public bool truncated
		{
			set{ _truncated=value;}
			get{return _truncated;}
		}
        /// <summary>
        /// ������Ϣ���ݲ������ͣ�
        /// </summary>
        public string geo
        {
            set { _geo = value; }
            get { return _geo; }
        }
		/// <summary>
		/// �ظ�ID
		/// </summary>
		public long in_reply_to_status_id
		{
			set{ _in_reply_to_status_id=value;}
			get{return _in_reply_to_status_id;}
		}
		/// <summary>
		/// �ظ���UserID
		/// </summary>
		public long in_reply_to_user_id
		{
			set{ _in_reply_to_user_id=value;}
			get{return _in_reply_to_user_id;}
		}
		/// <summary>
		/// �ظ����ǳ�
		/// </summary>
		public string in_reply_to_screen_name
		{
			set{ _in_reply_to_screen_name=value;}
			get{return _in_reply_to_screen_name;}
		}
		/// <summary>
		/// ����ͼ
		/// </summary>
		public string thumbnail_pic
		{
			set{ _thumbnail_pic=value;}
			get{return _thumbnail_pic;}
		}
		/// <summary>
		/// ����ͼƬ
		/// </summary>
		public string bmiddle_pic
		{
			set{ _bmiddle_pic=value;}
			get{return _bmiddle_pic;}
		}
		/// <summary>
		/// ԭʼͼƬ
		/// </summary>
		public string original_pic
		{
			set{ _original_pic=value;}
			get{return _original_pic;}
		}
		/// <summary>
		/// �û�ID
		/// </summary>
		public long user_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
        /// <summary>
        /// ͬʱת����΢��
        /// </summary>
        public Status retweeted_status
        {
            set { _retweeted_status = value; }
            get { return _retweeted_status; }
        }
        /// <summary>
        /// ����������Ĭ��Ϊ0��ÿ����һ�Σ��ͼ�1����Ϊ0��Ϊ���������
        /// </summary>
        public int iteration
        {
            set { _iteration = value; }
            get { return _iteration; }
        }
        /// <summary>
        /// ��¼����ʱ��
        /// </summary>
        public string update_time
        {
            set { _update_time = value; }
            get { return _update_time; }
        }
		#endregion Model

		#region  ��Ա����

		static public bool Exists(long lStatusID)
		{
            Database db = DatabaseFactory.CreateDatabase();
            db = DatabaseFactory.CreateDatabase();
            int count = db.CountByExecuteSQLSelect( "select count(status_id) from statuses where status_id=" + lStatusID.ToString() );
            return count > 0;
		}

        /// <summary>
        /// �������ݿ����������ݵĵ�������
        /// </summary>
        static public void NewIterate ()
        {
            Database db = DatabaseFactory.CreateDatabase();
            db = DatabaseFactory.CreateDatabase();
            db.CountByExecuteSQL( "update statuses set iteration=iteration+1" );
        }

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add()
		{
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                Hashtable htValues = new Hashtable();
                _update_time = "'" + DateTime.Now.ToString( "u" ).Replace( "Z", "" ) + "'";
                htValues.Add( "status_id", _status_id );
                htValues.Add( "created_at", "'" + _created_at + "'" );
                htValues.Add( "content", "'" + _content.Replace( "'", "''" ) + "'" );
                htValues.Add( "source_url", "'" + _source_url + "'" );
                htValues.Add( "source_name", "'" + _source_name + "'" );
                if (_favorited)
                    htValues.Add( "favorited", 1 );
                else
                    htValues.Add( "favorited", 0 );
                if (_truncated)
                    htValues.Add( "truncated", 1 );
                else
                    htValues.Add( "truncated", 0 );
                htValues.Add( "geo", "'" + _geo.Replace( "'", "''" ) + "'" );
                htValues.Add( "in_reply_to_status_id", _in_reply_to_status_id );
                htValues.Add( "in_reply_to_user_id", _in_reply_to_user_id );
                htValues.Add( "in_reply_to_screen_name", "'" + _in_reply_to_screen_name.Replace( "'", "''" ) + "'" );
                htValues.Add( "thumbnail_pic", "'" + _thumbnail_pic.Replace( "'", "''" ) + "'" );
                htValues.Add( "bmiddle_pic", "'" + _bmiddle_pic.Replace( "'", "''" ) + "'" );
                htValues.Add( "original_pic", "'" + _original_pic.Replace( "'", "''" ) + "'" );
                htValues.Add( "user_id", _user_id );
                if(_retweeted_status!=null)
                    htValues.Add( "retweeted_status_id", _retweeted_status.status_id );
                else
                    htValues.Add( "retweeted_status_id", 0 );
                htValues.Add( "iteration", iteration );
                htValues.Add( "update_time", _update_time );

                db.Insert( "statuses", htValues );
            }
            catch
            { return; }
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public void GetModel(long status_id)
		{}

        /// <summary>
        /// ������ݿ���ָ���û�����һ��΢��ID
        /// </summary>
        static public long GetLastStatusIDOf(long lUid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string strSQL = "select top 1 status_id from statuses where user_id="+lUid.ToString()+" order by created_at desc";
            DataRow dr = db.GetDataRow( strSQL );
            if (dr == null) return 0;
            else return Convert.ToInt64( dr[0] );
        }

		#endregion  ��Ա����
	}
}
