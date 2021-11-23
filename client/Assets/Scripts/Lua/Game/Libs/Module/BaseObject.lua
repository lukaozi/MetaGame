-------------------------------------------------
---
--- chenyukuan
--- 2021/11/22
-------------------------------------------------


---@class BaseObject:Object
local _M = class("BaseObject")

function _M.New(self)
    
    self:_Reset_();
end

function _M:_Reset_()
    ---@type BaseObject
    self._parent_ = nil
    ---@type BaseObject[]
    self._subs_ = nil
    
    self._isCreate_ = nil
    
    self._isEnable_ = false
    
    self._notEnableAfterCreate_ = false
    
    self._enableSelf_ = true
    
    ---@type TimerComponent
    self._timer_ = nil
    
    ---@type EventComponent
    self._eventComp_ = nil
    
    ---@type TweenComponent
    self._tween_ = nil
    
    self._isRecoving_ = false
end