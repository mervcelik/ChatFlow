using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Authorization.Enums;

public enum Permission : long
{
    None = 0,

    // --- Mesaj yetkileri ---
    SendMessage = 1 << 0,   // mesaj gönder
    EditOwnMessage = 1 << 1,   // kendi mesajını düzenle
    DeleteOwnMessage = 1 << 2,   // kendi mesajını sil
    DeleteAnyMessage = 1 << 3,   // başkasının mesajını sil (moderatör)
    PinMessage = 1 << 4,   // mesaj sabitle

    // --- Oda yetkileri ---
    ViewRoom = 1 << 5,   // odayı görüntüle
    InviteMember = 1 << 6,   // üye davet et
    RemoveMember = 1 << 7,   // üyeyi odadan çıkar
    UpdateRoomInfo = 1 << 8,   // oda adı/açıklama güncelle
    DeleteRoom = 1 << 9,   // odayı sil

    // --- Üye yetkileri ---
    PromoteMember = 1 << 10,  // üyeyi moderatöre yükselt
    DemoteModerator = 1 << 11,  // moderatörü üyeye düşür

    // --- Dosya yetkileri ---
    UploadFile = 1 << 12,  // dosya yükle
    DeleteAnyFile = 1 << 13,  // başkasının dosyasını sil
}
