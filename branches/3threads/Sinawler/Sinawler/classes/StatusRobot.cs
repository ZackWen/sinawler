using System;
using System.Collections.Generic;
using System.Text;
using Sina.Api;
using System.Threading;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Sinawler.Model;
using System.Data;

namespace Sinawler
{
    class StatusRobot:RobotBase
    {
        private LinkedList<long> lstRetweetedStatus = new LinkedList<long>();   //ת��΢��ID
        private long lRetweetedUID = 0;     //ת��΢����UID�����ڴ��ݸ��û�������

        public long CurrentSID
        { get { return lCurrentID; } }

        public long CurrentRetweetedUID
        { get { return lRetweetedUID; } }

        //���캯������Ҫ������Ӧ������΢��API��������
        public StatusRobot ( SinaApiService oAPI ):base(oAPI)
        {
            queueBuffer = new QueueBuffer( QueueBufferTarget.FOR_STATUS );
            strLogFile = Application.StartupPath + "\\" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_status.log";
        }

        /// <summary>
        /// ��ʼ��ȡ΢������
        /// </summary>
        public void Start()
        {
            //�������û����У���ȫ����UserRobot���ݹ���
            while (lstWaitingID.Count == 0) Thread.Sleep( 1 );   //������Ϊ�գ���ȴ�
            long lStartUID = lstWaitingID.First.Value;
            //�Զ�������ѭ�����У�ֱ���в�����ͣ��ֹͣ
            while(true)
            {
                if (blnAsyncCancelled) return;
                while (blnSuspending)
                {
                    if (blnAsyncCancelled) return;
                    Thread.Sleep(10);
                }
                //����ͷȡ��
                long lCurrentUID = lstWaitingID.First.Value;
                lstWaitingID.RemoveFirst();
                //�����ݿ���л���������Ԫ��
                long lHead = queueBuffer.Dequeue();
                if (lHead > 0)
                    lstWaitingID.AddLast( lHead );
                #region Ԥ����
                if (lCurrentUID == lStartUID)  //˵������һ��ѭ������
                {
                    if (blnAsyncCancelled) return;
                    while (blnSuspending)
                    {
                        if (blnAsyncCancelled) return;
                        Thread.Sleep(10);
                    }

                    //��־
                    strLog = DateTime.Now.ToString() + "  " + "��ʼ����֮ǰ���ӵ�������...";
                    bwAsync.ReportProgress(0);
                    Thread.Sleep(50);

                    Status.NewIterate();
                }
                #endregion                
                #region �û�΢����Ϣ
                if (blnAsyncCancelled) return;
                while (blnSuspending)
                {
                    if (blnAsyncCancelled) return;
                    Thread.Sleep(10);
                }
                //��־
                strLog = DateTime.Now.ToString() + "  " + "��ȡ���ݿ����û�" + lCurrentUID.ToString() + "����һ��΢����ID...";
                bwAsync.ReportProgress(0);
                Thread.Sleep(50);
                //��ȡ���ݿ��е�ǰ�û�����һ��΢����ID
                lCurrentID = Status.GetLastStatusIDOf( lCurrentUID );

                if (blnAsyncCancelled) return;
                while (blnSuspending)
                {
                    if (blnAsyncCancelled) return;
                    Thread.Sleep(10);
                }
                //��־
                strLog = DateTime.Now.ToString() + "  " + "��ȡ�û�" + lCurrentUID.ToString() + "��ID��" + lCurrentID.ToString() + "֮���΢��...";
                bwAsync.ReportProgress(0);
                Thread.Sleep(50);
                //��ȡ���ݿ��е�ǰ�û�����һ��΢����ID֮���΢�����������ݿ�
                List<Status> lstStatus = crawler.GetStatusesOfSince(lCurrentUID, lCurrentID);
                //��־
                strLog = DateTime.Now.ToString() + "  " + "����" + lstStatus.Count.ToString() + "��΢����";
                bwAsync.ReportProgress(0);
                Thread.Sleep(50);

                foreach (Status status in lstStatus)
                {
                    if (blnAsyncCancelled) return;
                    while (blnSuspending)
                    {
                        if (blnAsyncCancelled) return;
                        Thread.Sleep(10);
                    }
                    lCurrentID = status.status_id;
                    if (!Status.Exists(lCurrentID))
                    {
                        //��־
                        strLog = DateTime.Now.ToString() + "  " + "��΢��" + lCurrentID.ToString() + "�������ݿ�...";
                        bwAsync.ReportProgress(0);
                        Thread.Sleep(50);
                        status.Add();
                    }
                    //����΢����ת������ת��΢��ID���
                    if (status.retweeted_status_id > 0)
                    {
                        //��־
                        strLog = DateTime.Now.ToString() + "  " + "΢��" + lCurrentID.ToString() + "��ת��΢������ת��΢��" + status.retweeted_status_id.ToString() + "������еȴ���ȡ...";
                        bwAsync.ReportProgress(0);
                        Thread.Sleep(50);
                        lstRetweetedStatus.AddLast( status.retweeted_status_id );
                    }
                }
                #endregion                
                #region ��ȡ��ȡ��ת��΢��
                //��־
                strLog = DateTime.Now.ToString() + "  " + "����"+lstStatus.Count.ToString()+"��΢����ȡ��ϣ������"+lstRetweetedStatus.Count.ToString()+"��ת��΢����";
                bwAsync.ReportProgress(0);
                Thread.Sleep(50);
                if(lstRetweetedStatus.Count>0)
                {
                    //��־
                    strLog = DateTime.Now.ToString() + "  " + "��ʼ��ȡ��õ�" + lstRetweetedStatus.Count.ToString() + "��ת��΢��...";
                    bwAsync.ReportProgress(0);
                    Thread.Sleep(50);
                }
                while(lstRetweetedStatus.Count>0)
                {
                    if (blnAsyncCancelled) return;
                    while (blnSuspending)
                    {
                        if (blnAsyncCancelled) return;
                        Thread.Sleep(10);
                    }

                    //��������Ƶ��
                    crawler.AdjustFreq();
                    //��־
                    strLog = DateTime.Now.ToString() + "  " + "����������Ϊ" + crawler.SleepTime.ToString() + "���롣��Сʱʣ��" + crawler.ResetTimeInSeconds.ToString() + "�룬ʣ���������Ϊ" + crawler.RemainingHits.ToString() + "��";
                    bwAsync.ReportProgress( 0 );
                    Thread.Sleep( 50 );

                    lCurrentID = lstRetweetedStatus.First.Value;
                    lstRetweetedStatus.RemoveFirst();

                    Status status = crawler.GetStatus(lCurrentID);
                    if(status!=null)
                    {
                        //��¼ת��΢����UID
                        lRetweetedUID = status.uid;
                        if (!Status.Exists(lCurrentID))
                        {
                            //��־
                            strLog = DateTime.Now.ToString() + "  " + "��΢��" + lCurrentID.ToString() + "�������ݿ�...";
                            bwAsync.ReportProgress(0);
                            Thread.Sleep(50);
                            status.Add();
                        }
                        //����΢����ת������ת��΢��ID���
                        if (status.retweeted_status_id > 0)
                        {
                            //��־
                            strLog = DateTime.Now.ToString() + "  " + "΢��" + lCurrentID.ToString() + "��ת��΢������ת��΢��" + status.retweeted_status_id.ToString() + "����΢������...";
                            bwAsync.ReportProgress(0);
                            Thread.Sleep(50);
                            lstRetweetedStatus.AddLast( status.retweeted_status_id );
                        }
                    }
                }
                #endregion
                //����ٽ��ո��������UID�����β
                //��־
                strLog = DateTime.Now.ToString() + "  " + "�û�" + lCurrentUID.ToString() + "����������ȡ��ϣ���������β...";
                bwAsync.ReportProgress(0);
                Thread.Sleep(50);
                //���ڴ����Ѵﵽ���ޣ���ʹ�����ݿ���л���
                if (lstWaitingID.Count < iQueueLength)
                    lstWaitingID.AddLast( lCurrentUID );
                else
                    queueBuffer.Enqueue( lCurrentUID );
                //��������Ƶ��
                crawler.AdjustFreq();
                //��־
                strLog = DateTime.Now.ToString() + "  " + "����������Ϊ" + crawler.SleepTime.ToString() + "���롣��Сʱʣ��" + crawler.ResetTimeInSeconds.ToString() + "�룬ʣ���������Ϊ" + crawler.RemainingHits.ToString() + "��";
                bwAsync.ReportProgress(0);
                Thread.Sleep(50);
            }
        }

        public override void Initialize ()
        {
            //��ʼ����Ӧ����
            blnAsyncCancelled = false;
            blnSuspending = false;
            if (lstWaitingID != null) lstWaitingID.Clear();
            if (lstRetweetedStatus != null) lstRetweetedStatus.Clear();

            //������ݿ���л���
            queueBuffer.Clear();
        }
    }
}