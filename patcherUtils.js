const fs = require('fs');

if(!fs.existsSync('www/texturepacks')){
	fs.mkdirSync('www/texturepacks');
}

try{
	window.pconf = JSON.parse(fs.readFileSync('patcher.json'));
}catch(_){
	window.pconf = {
		texture_pack: 'none',
		keys: {
			PAUSE: 'p',
			SAVE: 's',
			RESTART: 'r',
			JS_CONSOLE: 'j',
			GOD_MODE: 'g',
			ADD_MONEY: 'm',
			NO_CLIP: 'n',
			SET_SPEED: 'e',
			SET_TEXTURE_PACK: 't',
			HELP: 'h'
		}
	}
	fs.writeFileSync('patcher.json', JSON.stringify(pconf, null, 4));
}

window.loadedTextures = [];
window.godMode = false;

window.addEventListener('keypress', e=>{
	switch(e.key){
		case pconf.keys.JS_CONSOLE: {
			let inp = prompt('Введите команду');
			if(!inp || inp.length == 0) return;
			try{
				alert(eval(inp));
			}catch(e){
				alert(e);
			}
			break;
		}

		case pconf.keys.SAVE: {
			SceneManager.push(Scene_Save);
			break;
		}

		case pconf.keys.GOD_MODE: {
			window.godMode = true;
			alert('Режим бога включен');
			break;
		}

		case pconf.keys.ADD_MONEY: {
			$gameParty._gold = Number.MAX_SAFE_INTEGER;
			alert('Деньги накручены');
			break;
		}

		case pconf.keys.PAUSE: {
			alert('ПАУЗА\n\nНажмите ОК, чтобы возобновить');
			break;
		}

		case pconf.keys.RESTART: {
			location.reload();
			break;
		}

		case pconf.keys.NO_CLIP: {
			$gamePlayer.canPass = ()=>true;
			break;
		}

		case pconf.keys.SET_SPEED: {
			let inp = prompt('Введите новую скорость', $gamePlayer._moveSpeed);
			if(inp && !isNaN(+inp)){
				$gamePlayer._moveSpeed = +inp;
			}
			break;
		}

		case pconf.keys.SET_TEXTURE_PACK: {
			let inp = prompt('Введите название текстурпака ["none" для отключения]', pconf.texture_pack);
			if(inp && inp.length > 0){
				pconf.texture_pack = inp;
				fs.writeFileSync('patcher.json', JSON.stringify(pconf, null, 4));
			}
			location.reload();
			break;
		}

		case pconf.keys.HELP: {
			let str = 'Клавиши RPGM Patcher\n\n';
			str += `[${pconf.keys.PAUSE.toUpperCase()}] Поставить игру на паузу\n`;
			str += `[${pconf.keys.RESTART.toUpperCase()}] Перезапустить игру\n`;
			str += `[${pconf.keys.JS_CONSOLE.toUpperCase()}] Открыть консоль JavaScript\n`;
			str += `[${pconf.keys.SAVE.toUpperCase()}] Открыть окно сохранения\n`;
			str += `[${pconf.keys.SET_TEXTURE_PACK.toUpperCase()}] Открыть окно выбора текстурпака`;
			str += `[${pconf.keys.GOD_MODE.toUpperCase()}] Режим бога (очень высокие HP, атака, защита и т.д.)\n`;
			str += `[${pconf.keys.ADD_MONEY.toUpperCase()}] Накрутка денег\n`;
			str += `[${pconf.keys.NO_CLIP.toUpperCase()}] NoClip (возможность проходить сквозь объекты)\n`;
			str += `[${pconf.keys.SET_SPEED.toUpperCase()}] Изменить скорость игрока\n`;
			alert(str);
		}
	}
});