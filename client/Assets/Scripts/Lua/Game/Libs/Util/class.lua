_classByName = {}

_singletonByName = {}

_hotFixMode = true

_hookMode = false

local objectClassName = "Object"

---@class Object
---@field _className_
---@field _type_
---@field _super_
---@field _isSingleton_
local Object = _classByName[objectClassName] or {}
_classByName[objectClassName] = Object
Object.__index = Object
Object._className_ = objectClassName
Object._type_ = Object
Object._super_ = nil
Object._isSingleton_ = nil

function Object.New(self, ...)
    if self == nil or rawget(self, "_className_") ~= nil then
        self = {}
        setmetatable(self, Object)
    end
    return self
end

function Object._recursiveNew_(self, ...)
    
end

function Object:toString()
    return self._className_
end

function Object:GetType()
    return self._type_
end

function Object:IsClass()
    return isClass(self)
end

function Object:IsObject()
    return isObject(self)
end

function isClass(class)
    return rawget(class, "_className_") ~= nil
end

function isObject(obj)
    return rawget(obj, "_className_") == nil and obj["_className_"] ~= nil
end

local classField = {_type_ = true, _super_= true,_className_ = true, typeDef=true, _isSingleton_ = true,
__index = true}

function class(className, super, isSingleton)
    if type(className) ~= "string" then
        print(type(className))
        error("className must be string")
    end

    if super == nil then
        super = Object
    elseif type(super) == "string" then
        super = _classByName[super]
        if super == nil then
            error("super error")
        end
    elseif not isClass(super) then
        error("super error2")
    end
    
    ---@type Object
    local class_type = _classByName[className] or {}
    _classByName[className] = class_type
    class_type._className_ = className
    class_type._type_ = class_type
    class_type._super_ = super
    class_type._isSingleton_ = isSingleton
    class_type._recursiveNew_ = nil
    class_type.__index = class_type

    if _hookMode then
        class_type.__newindex = function(t, k, v)
            if type(v) == "function" then
                v = HookFun(v, t._className_.."." .. k)
            end
            
            rawset(t, k, v)
        end
    end
    
    class_type.New = nil
    
    local setNewFun = function(funNew)
        if _hookMode then
            funNew = HookFun(funNew, className.."New")
        end
        
        rawset(class_type, "_originNew_", funNew)
        
        rawset(class_type, "New", function(obj, ...)

            if isSingleton then
                obj = _singletonByName[className]
                if obj == nil then
                    obj = {}
                    _singletonByName[className] = obj
                end
            else
                obj = {}
            end
            setmetatable(obj, class_type)
            
            super._recursiveNew_(obj, ...)
            funNew(obj, ...)
            return obj
            
        end)
        
        rawset(class_type, "_recursiveNew_", function(obj, ...)
            super._recursiveNew_(obj, ...)
            funNew(obj, ...)--真正的new
        end)

        if _hotFixMode then
            setmetatable(class_type, super)
        else
            for k, v in pairs(super) do
                local type = type(v)
                if type ~= "function" then
                    if not classField[k] then
                        error("出于性能考虑， 基类不能有非函数字段")
                    end
                else
                    if rawget(class_type, k) == nil then
                        rawset(class_type, k, v)
                    end
                end
            end
            
            setmetatable(class_type, nil) --都拷到子类了，不需要再继承了
        end
        
        return rawget(class_type, "New")
    end
    
    local temWaitFirstCall = {}
    temWaitFirstCall.__index = function(t, k)

        if k == "New" then
            return setNewFun(function() end)
        else
            return nil
        end
    end

    temWaitFirstCall.__newindex = function(t, k, v)
        if t ~= class_type or k ~= "New" then
            setNewFun(function() end)
            rawset(t, k, v)
            return
        end
        setNewFun(v)
    end
    
    setmetatable(class_type, temWaitFirstCall)
    
    return class_type
end

function singleton(className, super)
    return class(className, super, true)
end





















