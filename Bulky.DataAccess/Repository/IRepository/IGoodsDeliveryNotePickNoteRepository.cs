﻿using WMS.Models;

namespace WMS.DataAccess.Repository.IRepository
{
    public interface IGoodsDeliveryNotePickNoteRepository : IRepository<GoodsDeliveryNotePickNote>
    {
        void Update(GoodsDeliveryNotePickNote obj);
    }
}
