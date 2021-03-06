﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCenter.DTO;
using UserCenter.IServices;

namespace UserCenter.Services
{
    public class AppInfoService : IAppInfoService
    {
        public async Task<AppInfoDTO> GetByAppKeyAsync(string appKey)
        {
            using (UCDbContext ctx = new UCDbContext())
            {
                var appInfo = await ctx.AppInfos.FirstOrDefaultAsync(e => e.AppKey == appKey && e.IsEnabled == true);
                if (appInfo == null)
                {
                    return null;
                }
                else
                {
                    AppInfoDTO dto = new AppInfoDTO();
                    dto.AppKey = appInfo.AppKey;
                    dto.AppSecret = appInfo.AppSecret;
                    dto.Id = appInfo.Id;
                    dto.Name = appInfo.Name;
                    dto.IsEnabled = appInfo.IsEnabled;
                    return dto;
                }
            }
        }
    }
}
