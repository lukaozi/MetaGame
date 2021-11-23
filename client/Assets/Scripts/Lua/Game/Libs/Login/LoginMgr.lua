-------------------------------------------------
---
--- chenyukuan
--- 2021/11/22
-------------------------------------------------



---@class LoginMgr:Object
local _M = singleton("LoginMgr")

---@param self LoginMgr
---@return LoginMgr
function _M:New(self)
    self.reconnectHandler = self.reconnectHandler or require("recon")

end

return _M:New()