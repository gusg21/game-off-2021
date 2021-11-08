local tex = EI:LoadTex("penguin")

local x = 0
local y = 0

function Update()
    if x < 100 then
        x = x + 1
    end

    EI.Camera:Approach(this:Vector2(x + 4, y + 4), 1)
end

function Draw()
    EI.Drawing:Texture(tex, x, y)
end