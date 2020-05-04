(ns clojureturtle.fp-api
  (:require [clojureturtle.common :as common])
  (:require [clojureturtle.fp :as fp])
  (:require [clojure.core.match :refer [match]])
  (:require [failjure.core :as f])
  (:require [clojure.string :refer (split triml)]))



(defn parse-float [x] (Float/parseFloat x))
(defn validate-helper [vaildate-fun argument]
  (try
    (vaildate-fun argument)
    (catch Exception e
       (f/fail (ex-message e)))))

(defn validate-distance [distance-str]
  (validate-helper parse-float distance-str))

(defn validate-angle [angle-str]
  (validate-helper parse-float angle-str))

(defn validate-color [color-str]
  (letfn [(match-color [color]
            (case color
              "Black" :Black
              "Blue" :Blue
              "Red" :Red))]

    (validate-helper match-color color-str)))

(def turtle (fp/make-turtle))

(defn exec [command]
  (let [token (-> command
                  (triml)
                  (split #"\s+"))]
    (match token
      ["Move" distance] (f/ok->> (validate-distance distance)
                             (fp/move turtle))
      ["Turn" angle] (f/ok->> (validate-angle angle)
                          (fp/turn turtle))
      ["Pen" "Up"] (fp/pen-up turtle)
      ["Pen" "Down"] (fp/pen-down turtle)
      ["SetColor" color] (f/ok->> (validate-color color)
                              (fp/set-color turtle))
      :else (f/fail "unmatched"))))

(defn draw-polygon [n]
  (let [angle (- 180 (/ 360 n))
        one-side (fn []
                   (do (exec "Move 100")
                       (exec (str "Turn " angle))))]
    (map #(%)  (repeat n one-side))))


(defn trigger-error [] (exec "Move bad"))