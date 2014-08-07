using System;
using System.ServiceModel.DomainServices.Client;
using ScheduleViewDB.Web;

namespace ScheduleViewDB
{
	public class ScheduleViewRepository
	{
		private static ScheduleViewDomainContext context;

		static ScheduleViewRepository() { }

		public static ScheduleViewDomainContext Context
		{
			get
			{
				if (context == null)
				{
					context = new ScheduleViewDomainContext();
				}

				return context;
			}
		}

		public static bool SaveData(Action action)
		{
            if (ScheduleViewRepository.Context.HasChanges && !ScheduleViewRepository.Context.IsSubmitting)
            {
                try
                {
                    ScheduleViewRepository.Context.SubmitChanges(OnSubmitChangesCompleted, action);
                    return true;
                }
                catch (System.Exception)
                {
                    return false;
                }
            }

            if (action != null)
            {
                action();
            }

            return false;
		}

		private static void OnSubmitChangesCompleted(SubmitOperation submitOperation)
		{
			var action = submitOperation.UserState as Action;

			if (action != null)
			{
				action();
			}

			if (submitOperation.HasError)
			{				
				if (submitOperation.Error is DomainOperationException)
				{
					DomainOperationException exception = (DomainOperationException)submitOperation.Error;

					if (exception.Status == OperationErrorStatus.Conflicts)
					{
						foreach (var item in submitOperation.EntitiesInError)
						{
							if (item.EntityConflict.IsDeleted)
							{
								foreach (var entitySet in ScheduleViewRepository.Context.EntityContainer.EntitySets)
								{
									if (entitySet.EntityType == item.GetType())
									{
										entitySet.Detach(item);
										entitySet.Attach(item);
										entitySet.Remove(item);
										ScheduleViewRepository.Context.SubmitChanges();
									}
								}
							}
							else
							{
								item.EntityConflict.Resolve();
							}
						}
					}
					submitOperation.MarkErrorAsHandled();
				}
			}
		}
	}
}
