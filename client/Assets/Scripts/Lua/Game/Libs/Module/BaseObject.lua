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

function _M:SetParentComponent(Parent)
    self._parent_ = Parent
end

function _M:GetParentComponent()
    return self._parent_
end

function _M:AddSubComponentByObj(subObj, class, ...)
    if not self._isCreate_ then
        error("AddSubComponentByObj error")
    end

    if not self._subs_ then
        self._subs_ = {}
    end
    
    local sub = subObj == nil and class:New() or subObj
    if sub.SetParentComponent == nil then
        error( " AddSubComponentByObj error2")
    end
    sub:SetParentComponent(self)
    table.insert(self._subs_, sub)
    sub:Create()
end

