using InternshipHMSModels.Models;
using InternshipHMSService.Persistance;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace InternshipHMSWeb.Jobs
{
    public class ReservationJobs : IJob
    {
        
        public async Task Execute(IJobExecutionContext context)
        {
            
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            Guid ReserveID = Guid.Parse(dataMap.GetString("ReserveID"));

            UnitOfWork unitOfWork = new UnitOfWork(new InternshipHMSDataAccess.ApplicationDbContext());

            Reservation reservation = unitOfWork.Reservation_List.Find(ReserveID);
            if (reservation.ReservationStateID.ToString() == "4c825c2b-ad88-48d5-af69-277d97351651")
                reservation.ReservationStateID = Guid.Parse("4c825c2b-ad46-48d5-af69-277d97396441");
            unitOfWork.Reservation_List.Update(reservation);
            unitOfWork.Complete();
        }
        
        public static async void Start(Guid reservationID,DateTime dateTime)
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();

            await scheduler.Start();
            IJobDetail job = JobBuilder.Create<ReservationJobs>()
                .UsingJobData("ReserveID", reservationID.ToString())
                .Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("TriggerOrderMinus", $"Job_{reservationID.ToString()}")
                .StartAt(dateTime)
                .Build();
            bool flag = await scheduler.CheckExists(trigger.Key);
            if (flag)
            {
                await scheduler.RescheduleJob(trigger.Key, trigger);
            }
            else
            {
                await scheduler.ScheduleJob(job, trigger);
            }
        }
    }
}