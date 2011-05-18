using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Sinawler.Model
{
	/// <summary>
    /// ��Comment�������еķ��������������ݿ⽻�������ж�ȡ��������д������
	/// </summary>
    /// 
    /*
     <comment>
        <created_at>Thu Sep 16 19:47:22 +0800 2010</created_at> 
        <id>3311494857</id> 
        <text>�ظ�@����:����2</text> 
        + <user>
        + <status>
    </comment>
    */
    public class Comment
	{
        #region Model

		private long _comment_id;
		private string _content;
		private string _created_at;
		private long _user_id;
		private long _status_id;
		private int _iteration;
        private string _update_time;
		/// <summary>
		/// ����ID��XML��Ϊid��
		/// </summary>
		public long comment_id
		{
			set{ _comment_id=value;}
			get{return _comment_id;}
		}
		/// <summary>
		/// �������ݣ�XML��Ϊtext��
		/// </summary>
		public string content
		{
			set{ _content=value;}
			get{return _content;}
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
		/// ������ID
		/// </summary>
		public long user_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		/// <summary>
		/// ���۵�΢��
		/// </summary>
		public long status_id
		{
			set{ _status_id=value;}
			get{return _status_id;}
		}
		/// <summary>
        /// ����������Ĭ��Ϊ0��ÿ����һ�Σ��ͼ�1����Ϊ0��Ϊ���������
        /// </summary>
        public int iteration
        {
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

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public Comment()
        { }

		/// <summary>
		/// ���ݿ����Ƿ����ָ��ID������
		/// </summary>
		static public bool Exists(long lCommentID)
		{
            Database db = DatabaseFactory.CreateDatabase();
            int count = db.CountByExecuteSQLSelect( "select count(comment_id) from comments where comment_id=" + lCommentID.ToString() );
            return count > 0;
		}

        /// <summary>
        /// �������ݿ����������ݵĵ�������
        /// </summary>
        static public void NewIterate ()
        {
            Database db = DatabaseFactory.CreateDatabase();
            db.CountByExecuteSQL( "update comments set iteration=iteration+1" );
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
                htValues.Add( "comment_id", _comment_id );
                htValues.Add( "created_at", "'" + _created_at + "'" );
                htValues.Add( "content", "'" + _content.Replace( "'", "''" ) + "'" );
                htValues.Add( "user_id", _user_id );
                htValues.Add( "status_id", _status_id );
                htValues.Add( "iteration", 0 );
                htValues.Add( "update_time", _update_time );

                db.Insert( "comments", htValues );
            }
            catch
            { return; }
		}

		#endregion  ��Ա����
	}
}
