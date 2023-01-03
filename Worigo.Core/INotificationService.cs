using CorePush.Apple;
using CorePush.Google;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using static Worigo.Core.GoogleNotification;

namespace Worigo.Core
{
    public interface INotificationService
    {
        Task<ResponseModel> SendNotification(NotificationModel notificationModel);
        public class NotificationService : INotificationService
        {
            public async Task<ResponseModel> SendNotification(NotificationModel notificationModel)
            {
                ResponseModel response = new ResponseModel();
                try
                {
                    if (notificationModel.IsAndroiodDevice)
                    {
                        /* FCM Sender (Android Device) */
                        FcmSettings settings = new FcmSettings()
                        {
                            SenderId = "971453447570",
                            ServerKey = "AAAA4i8jPZI:APA91bHBhbZv8jxudv7aqZjGk7-uRHesH_AJq1w0at_biZsZtVIt_VtlLCdzV9kuiW7WbqcCXiIOcpZRFy0BN2wuZJRLJrry8gfLmven_QXCoMLrZgYrBNMp2Xnlup6MwBXJJDagt3rD"
                        };
                        HttpClient httpClient = new HttpClient();

                        string authorizationKey = string.Format("keyy={0}", settings.ServerKey);
                        string deviceToken = notificationModel.DeviceId;

                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
                        httpClient.DefaultRequestHeaders.Accept
                                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        DataPayload dataPayload = new DataPayload();
                        dataPayload.Title = notificationModel.Title;
                        dataPayload.Body = notificationModel.Body;

                        GoogleNotification notification = new GoogleNotification();
                        notification.Data = dataPayload;
                        notification.Notification = dataPayload;

                        var fcm = new FcmSender(settings, httpClient);
                        var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);

                        if (fcmSendResponse.IsSuccess())
                        {
                            response.IsSuccess = true;
                            response.Message = "Notification sent successfully";
                            return response;
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = fcmSendResponse.Results[0].Error;
                            return response;
                        }
                    }
                    else
                    {
                        /* Code here for APN Sender (iOS Device) */
                        var s = new ApnSettings
                        {
                            AppBundleIdentifier = "com.worigo.worigohotel",
                            ServerType = ApnServerType.Development,
                            TeamId = "5ZV65N3AZ5",
                            P8PrivateKey = "NotiPush",
                            P8PrivateKeyId = "JGW9DYJXKK",

                        };
                        var httpClient = new HttpClient();
                        var serverId = "AAAAB20JOos:APA91bFY7J_UCXOW-Zy-6pBVdW1tjMFQ_8FJdbZJxpEVoP3Ig322xyTR4qqcDtTrf07yxN9HHjZ3Xr-kyAh-B-Ft7vRfJvivX01-iXIs8iODYluk-ZpNRzHe227ZOjEFki2u_VMz8WyQ";
                        var authorizationKey = string.Format("keyy={0}", serverId);
                        string deviceToken = notificationModel.DeviceId;

                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
                        httpClient.DefaultRequestHeaders.Accept
                                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        AppleNotification app = new AppleNotification(Guid.NewGuid(), "Test", "dsd");


                        var apn = new ApnSender(s, httpClient);


                        var value = apn.SendAsync(app, notificationModel.DeviceId);
                    }
                    return response;
                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.Message = "Something went wrong";
                    return response;
                }
            }

        }
    }
}