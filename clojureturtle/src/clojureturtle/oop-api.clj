(ns clojureturtle.oop-api
  (:require [clojureturtle.common :as common])
  (:require [clojureturtle.oop :as oop])
  (:require [clojure.string :refer (split triml)])
  (:require [clojure.core.match :refer [match]]))

(defn parse-float [x] (Float/parseFloat x))
(defn vaildate-helper [vaildate-fun argument ex-message ex-key]
  (try
    (vaildate-fun argument)
    (catch Exception e
      (throw (ex-info ex-message {ex-key argument})))))

(defn validate-distance [distance-str]
  (vaildate-helper parse-float distance-str "Invaild distance" :distance))

(defn validate-angle [angle-str]
  (vaildate-helper parse-float angle-str "Invaild angle" :angle))

(defn validate-color [color-str]
  (letfn [(match-color [color]
            (case color
              "Black" :Black
              "Blue" :Blue
              "Red" :Red))]

    (vaildate-helper match-color color-str "Invaild color" :color)))

(def turtle (oop/make-turtle))

(defn exec [command]
  (let [token (-> command
                  (triml)
                  (split #"\s+"))]
    (match token
      ["Move" distance] (->> (validate-distance distance)
                             (oop/move turtle))
      ["Turn" angle] (->> (validate-angle angle)
                          (oop/turn turtle))
      ["Pen" "Up"] (oop/pen-up turtle)
      ["Pen" "Down"] (oop/pen-down turtle)
      ["SetColor" color] (->> (validate-color color)
                              (oop/set-color turtle))
      :else (throw (ex-info "unmatced" {:token token})))))


(defn draw-polygon [n]
  (let [angle (- 180 (/ 360 n))
        one-side (fn []
                   (do (exec "Move 100")
                       (exec (str "Turn " angle))))]
    (map #(%)  (repeat n one-side))))